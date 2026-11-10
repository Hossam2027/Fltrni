import { Component, OnInit } from '@angular/core';
import { ICreateProvider } from '../Interfaces/IPostProvider';
import { ICategory } from '../Interfaces/ICategory';
import { ServiceproviderserviceService } from '../serviceprovider/serviceproviderservice.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { TranslateModule } from '@ngx-translate/core';
@Component({
  selector: 'app-addserviceprovider',
  standalone: true,
  imports: [CommonModule, FormsModule, TranslateModule],
  templateUrl: './addserviceprovider.component.html',
  styleUrl: './addserviceprovider.component.scss'
})
export class AddserviceproviderComponent implements OnInit {
  createProvider: ICreateProvider = {
    name: '',
    phone: '',
    governorate: '',
    area: '',
    address: '',
    startingPrice: 0,
    latitude: 0,
    longitude: 0,
    categoryId: 0
  };
  categories: ICategory[] = [];
  categoryMode: 'existing' | 'new' = 'existing';
  newCategoryName = '';
  constructor(private serviceProviderService: ServiceproviderserviceService, private toastr: ToastrService) { }
  ngOnInit(): void {
    this.serviceProviderService.getCategories().subscribe({
      next: (categories) => {
        console.log('Categories API response:', categories);
        this.categories = categories;
      },
      error: (error) => {
        console.error('Error fetching categories:', error);
      }
    });
  }


  addServiceProvider() {
    const isNewCategory = this.createProvider.categoryId === -1;
    if (isNewCategory && !this.newCategoryName.trim()) {
      return;
    }

    const providerData: ICreateProvider = {
      ...this.createProvider, newCategoryName: isNewCategory ? this.newCategoryName.trim() : null
    };

    this.serviceProviderService.createServiceProvider(providerData).subscribe({
      next: (response) => {
        console.log('Service Provider created:', response);
        this.toastr.success(
          '  تسجيل طلبك بنجاح , جاري الموافقه عليه   ',
        );
      },
      error: (error) => {
        console.error('Error creating service provider:', error);
        this.toastr.error(
          '  حدث خطأ أثناء إنشاء مزود الخدمة   ',
        );
      }
    });
  }
}
