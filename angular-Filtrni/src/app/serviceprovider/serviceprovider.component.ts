
import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule, ActivatedRoute } from '@angular/router';
import { TranslateModule } from '@ngx-translate/core';

import { ServiceproviderserviceService } from './serviceproviderservice.service';
import { Iprovider } from '../Interfaces/Iprovider';

@Component({
  selector: 'app-serviceprovider',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    RouterModule,
    TranslateModule
  ],
  templateUrl: './serviceprovider.component.html',
  styleUrl: './serviceprovider.component.scss'
})
export class ServiceproviderComponent implements OnInit {

  // Providers
  providers: Iprovider[] = [];

  // Filters
  search = '';
  governorate = '';
  area = '';
  categoryId = 0;
  categories: any[] = [];
  governorates: string[] = [];
  areas: string[] = [];

  // Sorting
  sortBy = 'recommended';

  // Nearest provider
  nearestError = '';
  nearestLoading = false;

  // User location
  userLatitude: number | null = null;
  userLongitude: number | null = null;

  locationLoading = false;
  locationError = '';

  // Pagination
  pageSize = 12;
  currentPage = 1;
  totalCount = 0;

  private lastSearch = '';

  constructor(
    private serviceProviderService: ServiceproviderserviceService,
    private route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    // Read search parameters from the URL.
    this.route.queryParamMap.subscribe(params => {
      const newSearch = params.get('search') ?? '';

      if (newSearch !== this.lastSearch) {
        this.search = newSearch;
        this.lastSearch = newSearch;
        this.resetPagination();
      }

      this.getallServiceProviders();
    });

    this.getcategories();
    this.loadLocations();
    this.loadFilteredAreas();
    this.getUserLocation();
  }

  // =========================
  // Categories
  // =========================

  getcategories(): void {
    this.serviceProviderService.getCategories().subscribe({
      next: response => {
        this.categories = response;
        // this.categories = Array.isArray(response)
        //   ? response
        //   : [];
      },
      error: error => {
        console.error('Error loading categories:', error);
        this.categories = [];
      }
    });
  }

  // =========================
  // Get providers with pagination
  // =========================

  getallServiceProviders(): void {
    this.serviceProviderService.getServiceProviders(
      this.search,
      this.governorate,
      this.area,
      this.categoryId,
      this.currentPage,
      this.pageSize
    ).subscribe({
      next: (response: any) => {
        console.log('Pagination API response:', response);

        // يدعم الاستجابة القديمة والجديدة
        if (Array.isArray(response)) {
          this.providers = response;
          this.totalCount = response.length;
        } else {
          this.providers = Array.isArray(response?.data)
            ? response.data
            : [];

          this.totalCount = Number(response?.count ?? 0);
          this.currentPage = Number(
            response?.pageIndex ?? this.currentPage
          );
          this.pageSize = Number(
            response?.pageSize ?? this.pageSize
          );
        }

        this.calculateProviderDistances();


      },
      error: error => {
        console.error('Error loading service providers:', error);
        this.providers = [];
        this.totalCount = 0;
      }
    });
  }
  // =========================
  // Sorting
  // =========================

  get sortedProviders(): Iprovider[] {
    const result = Array.isArray(this.providers)
      ? [...this.providers]
      : [];

    switch (this.sortBy) {
      case 'rating':
        return result.sort(
          (a, b) => (b.rating ?? 0) - (a.rating ?? 0)
        );

      case 'priceLow':
        return result.sort(
          (a, b) =>
            (a.startingPrice ?? 0) - (b.startingPrice ?? 0)
        );

      case 'priceHigh':
        return result.sort(
          (a, b) =>
            (b.startingPrice ?? 0) - (a.startingPrice ?? 0)
        );

      case 'name':
        return result.sort(
          (a, b) =>
            (a.name ?? '').localeCompare(b.name ?? '')
        );

      case 'distance':
        return result.sort((a, b) => {
          const distanceA = (a as any).distance;
          const distanceB = (b as any).distance;

          if (distanceA == null && distanceB == null) {
            return 0;
          }

          if (distanceA == null) {
            return 1;
          }

          if (distanceB == null) {
            return -1;
          }

          return distanceA - distanceB;
        });

      default:
        return result;
    }
  }

  // The API already returns the current page.
  // Do not slice the array again here.
  get paginatedProviders(): Iprovider[] {
    return this.sortedProviders;
  }

  // =========================
  // Pagination
  // =========================

  get totalPages(): number {
    return Math.ceil(this.totalCount / this.pageSize);
  }

  get pageNumbers(): number[] {
    return Array.from(
      { length: this.totalPages },
      (_, index) => index + 1
    );
  }

  goToPage(page: number): void {
    if (
      page < 1 ||
      page > this.totalPages ||
      page === this.currentPage
    ) {
      return;
    }

    this.currentPage = page;
    this.getallServiceProviders();
  }

  resetPagination(): void {
    this.currentPage = 1;
  }

  // =========================
  // Filters
  // =========================

  resetFilters(): void {
    this.search = '';
    this.governorate = '';
    this.area = '';
    this.categoryId = 0;
    this.sortBy = 'recommended';

    this.resetPagination();
    this.loadFilteredAreas();
    this.getallServiceProviders();
  }

  onGovernorateChange(): void {
    this.area = '';

    this.resetPagination();
    this.loadFilteredAreas();
    this.getallServiceProviders();
  }

  onCategoryChange(): void {
    this.governorate = '';
    this.area = '';

    this.resetPagination();
    this.loadFilteredAreas();
    this.getallServiceProviders();
  }

  // =========================
  // Locations and areas
  // =========================

  loadLocations(): void {
    this.serviceProviderService.getServiceProviders().subscribe({
      next: (response: any) => {
        const providers: any[] = Array.isArray(response)
          ? response
          : Array.isArray(response?.data)
            ? response.data
            : Array.isArray(response?.items)
              ? response.items
              : [];

        this.governorates = Array.from(
          new Set(
            providers
              .map(provider => provider.governorate)
              .filter(
                (value): value is string =>
                  typeof value === 'string' &&
                  value.trim() !== ''
              )
              .map(value => value.trim())
          )
        ).sort((a, b) => a.localeCompare(b));
      },
      error: error => {
        console.error('Error loading locations:', error);
        this.governorates = [];
      }
    });
  }

  loadFilteredAreas(): void {
    this.serviceProviderService
      .getAreas(this.categoryId, this.governorate).subscribe({
        next: areas => {
          this.areas = Array.isArray(areas) ? areas : [];
          if (this.area && !this.areas.includes(this.area)) {
            this.area = '';
          }
        },
        error: error => {
          console.error('Error loading filtered areas:', error);
          this.areas = [];
        }
      });
  }

  // =========================
  // User location
  // =========================

  getUserLocation(): void {
    if (!navigator.geolocation) {
      this.locationError =
        'المتصفح لا يدعم تحديد الموقع.';
      return;
    }

    this.locationLoading = true;
    this.locationError = '';

    navigator.geolocation.getCurrentPosition(
      position => {
        this.userLatitude = position.coords.latitude;
        this.userLongitude = position.coords.longitude;

        this.locationLoading = false;
        this.calculateProviderDistances();
      },
      error => {
        this.locationLoading = false;

        if (error.code === error.PERMISSION_DENIED) {
          this.locationError =
            'اسمح للمتصفح بالوصول إلى موقعك لحساب المسافات.';
        } else if (
          error.code === error.POSITION_UNAVAILABLE
        ) {
          this.locationError = 'موقعك الحالي غير متاح.';
        } else {
          this.locationError =
            'تعذر تحديد موقعك. حاول مرة أخرى.';
        }
      },
      {
        enableHighAccuracy: true,
        timeout: 15000,
        maximumAge: 60000
      }
    );
  }

  // =========================
  // Calculate provider distances
  // =========================

  private calculateProviderDistances(): void {
    if (!Array.isArray(this.providers)) {
      this.providers = [];
      return;
    }

    if (
      this.userLatitude === null ||
      this.userLongitude === null
    ) {
      return;
    }

    this.providers = this.providers.map(provider => {
      const latitude = Number(provider.latitude);
      const longitude = Number(provider.longitude);

      if (
        provider.latitude == null ||
        provider.longitude == null ||
        !Number.isFinite(latitude) ||
        !Number.isFinite(longitude) ||
        latitude < -90 ||
        latitude > 90 ||
        longitude < -180 ||
        longitude > 180
      ) {
        return {
          ...provider,
          distance: null
        } as Iprovider;
      }

      const distance = this.calculateDistance(
        this.userLatitude!,
        this.userLongitude!,
        latitude,
        longitude
      );

      return {
        ...provider,
        distance
      } as Iprovider;
    });
  }

  // =========================
  // Find nearest provider
  // =========================

  getNearestProvider(): void {
    this.nearestError = '';

    if (
      this.userLatitude !== null &&
      this.userLongitude !== null
    ) {
      this.calculateProviderDistances();

      const hasCoordinates = this.providers.some(
        provider => (provider as any).distance != null
      );

      if (!hasCoordinates) {
        this.nearestError =
          'لا يوجد مقدمو خدمات لديهم إحداثيات مسجلة.';
        return;
      }

      this.sortBy = 'distance';
      return;
    }

    if (!navigator.geolocation) {
      this.nearestError =
        'المتصفح لا يدعم تحديد الموقع.';
      return;
    }

    this.nearestLoading = true;

    navigator.geolocation.getCurrentPosition(
      position => {
        this.userLatitude = position.coords.latitude;
        this.userLongitude = position.coords.longitude;

        this.calculateProviderDistances();
        this.nearestLoading = false;

        const hasCoordinates = this.providers.some(
          provider => (provider as any).distance != null
        );

        if (!hasCoordinates) {
          this.nearestError =
            'لا يوجد مقدمو خدمات لديهم إحداثيات مسجلة.';
          return;
        }

        this.sortBy = 'distance';
      },
      error => {
        this.nearestLoading = false;

        this.nearestError =
          error.code === error.PERMISSION_DENIED
            ? 'اسمح للمتصفح بالوصول إلى موقعك أولاً.'
            : 'تعذر تحديد موقعك. حاول مرة أخرى.';
      },
      {
        enableHighAccuracy: true,
        timeout: 15000,
        maximumAge: 60000
      }
    );
  }

  // =========================
  // Haversine distance formula
  // =========================

  private calculateDistance(
    lat1: number,
    lon1: number,
    lat2: number,
    lon2: number
  ): number {
    const toRadians = (degrees: number): number =>
      degrees * Math.PI / 180;

    const dLat = toRadians(lat2 - lat1);
    const dLon = toRadians(lon2 - lon1);

    const a =
      Math.sin(dLat / 2) ** 2 +
      Math.cos(toRadians(lat1)) *
      Math.cos(toRadians(lat2)) *
      Math.sin(dLon / 2) ** 2;

    const safeA = Math.min(1, Math.max(0, a));

    return (
      6371 *
      2 *
      Math.atan2(
        Math.sqrt(safeA),
        Math.sqrt(1 - safeA)
      )
    );
  }
}
