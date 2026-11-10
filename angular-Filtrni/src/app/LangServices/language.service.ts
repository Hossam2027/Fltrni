
import { Injectable, Inject, PLATFORM_ID } from '@angular/core';
import { DOCUMENT, isPlatformBrowser } from '@angular/common';
import { TranslateService } from '@ngx-translate/core';

@Injectable({
  providedIn: 'root'
})
export class LanguageService {

  private isBrowser: boolean;

  constructor(
    private translate: TranslateService,
    @Inject(DOCUMENT) private document: Document,
    @Inject(PLATFORM_ID) platformId: object
  ) {
    this.isBrowser = isPlatformBrowser(platformId);

    this.translate.addLangs(['ar', 'en']);
    this.translate.setDefaultLang('en');

    // Start with Arabic
    this.translate.use('en');

    if (this.isBrowser) {
      this.applyDirection('en');
    }

    // Update direction whenever the language changes
    this.translate.onLangChange.subscribe(event => {
      if (this.isBrowser) {
        this.applyDirection(event.lang);
      }
    });
  }

  get currentLang(): string {
    return this.translate.currentLang || 'en';
  }

  toggleLanguage(): void {
    const lang = this.currentLang === 'ar' ? 'en' : 'ar';

    this.translate.use(lang);
  }

  private applyDirection(lang: string): void {
    const isArabic = lang === 'ar';

    this.document.documentElement.lang = lang;
    this.document.documentElement.dir = isArabic ? 'rtl' : 'ltr';

    this.document.body.classList.toggle('lang-ar', isArabic);
    this.document.body.classList.toggle('lang-en', !isArabic);
  }
  setLanguage(lang: 'en' | 'ar'): void {
    this.translate.use(lang).subscribe({
      next: () => {
        console.log('Language loaded:', lang);
        console.log(
          'Translation test:',
          this.translate.instant('NAV.LOGIN')
        );
      },
      error: (err) => {
        console.error('Failed to load language:', lang, err);
      }
    });
  }
}
