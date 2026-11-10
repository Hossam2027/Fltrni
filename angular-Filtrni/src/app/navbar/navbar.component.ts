import { Component, OnInit, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavigationEnd, Router, RouterLink, RouterLinkActive } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { Subscription, filter } from 'rxjs';
import { TranslateModule } from '@ngx-translate/core';
import { AuthService } from '../auth/services/auth.service';
import { ProviderservicesService } from '../providerstatus/providerservices.service';
import { LanguageService } from '../LangServices/language.service';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [
    CommonModule, RouterLink, RouterLinkActive,
    FormsModule, TranslateModule
  ],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss'
})
export class NavbarComponent implements OnInit, OnDestroy {

  userName: string | null = null;
  search = '';
  isServiceProviderPage = false;
  isMenuOpen = false;
  showProviderStatusButton = false;
  languageMenuOpen = false;
  private subscriptions = new Subscription();

  constructor(public languageService: LanguageService, public authService: AuthService, public router: Router,
    public providerstatusService: ProviderservicesService) {
    this.updateCurrentPage();

    this.subscriptions.add(this.router.events.pipe(filter((event): event is NavigationEnd =>
      event instanceof NavigationEnd)).subscribe(() => {
        this.updateCurrentPage();
        this.closeMenu();
      })
    );
  }

  ngOnInit(): void {
    this.subscriptions.add(this.authService.userName$.subscribe(name => {
      this.userName = name;
      if (name && this.authService.isLoggedIn()) {
        this.checkProviderStatus();
      } else {
        this.showProviderStatusButton = false;
      }
    })
    );
  }

  private updateCurrentPage(): void {
    const currentUrl = this.router.url.split('?')[0].split('#')[0];
    this.isServiceProviderPage = currentUrl === '/serviceprovider';
  }

  toggleMenu(): void {
    this.isMenuOpen = !this.isMenuOpen;
  }

  closeMenu(): void {
    this.isMenuOpen = false;
  }

  onSearch(): void {
    const searchTerm = this.search.trim();

    this.router.navigate(['/serviceprovider'], {
      queryParams: { search: searchTerm || null }
    }).then(success => {
      if (success) {
        this.search = '';
        this.closeMenu();
      }
    });
  }

  toggleLanguage(): void {
    this.languageService.toggleLanguage();
    this.closeMenu();
  }

  hasRole(role: string): boolean {
    return this.authService.getUserRole() === role;
  }

  logout(): void {
    this.authService.logout();
    this.closeMenu();
  }

  checkProviderStatus(): void {
    if (!this.authService.isLoggedIn()) {
      this.showProviderStatusButton = false;
      return;
    }

    this.providerstatusService.getProviderStatus().subscribe({
      next: (response: any) => {
        const status = Number(response?.status ?? response?.Status);
        this.showProviderStatusButton = status === 0;
      },
      error: error => {
        console.error('Error fetching provider status:', error);
        this.showProviderStatusButton = false;
      }
    });
  }

  goToProviderStatus(): void {
    this.router.navigate(['/providerstatus']);
    this.closeMenu();
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }
  setLanguage(lang: 'ar' | 'en'): void {
    this.languageService.setLanguage(lang);
    this.closeMenu();
  }
}