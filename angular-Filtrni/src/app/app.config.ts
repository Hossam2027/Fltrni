
import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';
import { routes } from './app.routes';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import {
  TranslateLoader,
  TranslateModule
} from '@ngx-translate/core';
import { importProvidersFrom } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {
  provideClientHydration
} from '@angular/platform-browser';
import { provideAnimations } from '@angular/platform-browser/animations';

import {
  provideHttpClient,
  withFetch,
  withInterceptors
} from '@angular/common/http';

import { authInterceptor } from './Interceptor/interceptors/auth.interceptor';
import { provideToastr } from 'ngx-toastr';
export function createTranslateLoader(
  http: HttpClient
) {
  return new TranslateHttpLoader(
    http,
    './assets/i18n/',
    '.json'
  );
}
export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),

    provideClientHydration(),

    provideHttpClient(
      withFetch(),
      withInterceptors([authInterceptor])
    ),

    importProvidersFrom(
  TranslateModule.forRoot({
    defaultLanguage: 'ar',
    loader: {
      provide: TranslateLoader,
      useFactory: createTranslateLoader,
      deps: [HttpClient]
    }
  })
),
    provideAnimations(),

    provideToastr({
      positionClass: 'toast-top-right',

      timeOut: 2000,
      extendedTimeOut: 2000,

      closeButton: true,
      progressBar: true,
      progressAnimation: 'decreasing',

      newestOnTop: true,
      preventDuplicates: true,
      tapToDismiss: true,

      toastClass: 'ngx-toastr fltrni-toast',
      titleClass: 'toast-title',
      messageClass: 'toast-message',

      enableHtml: false,
      easeTime: 250
    })
  ]
};
