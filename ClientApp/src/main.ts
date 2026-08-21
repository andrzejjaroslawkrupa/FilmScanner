/***************************************************************************************************
 * Load `$localize` onto the global scope - used if i18n tags appear in Angular templates.
 */
import { importProvidersFrom } from '@angular/core';
import { bootstrapApplication } from '@angular/platform-browser';

import { AppComponent } from './app/app.component';
import { AppModule } from './app/app.module';
import { environment } from './environments/environment';

export function getBaseUrl() {
  return document.getElementsByTagName('base')[0].href;
}

bootstrapApplication(AppComponent, {
  providers: [
    importProvidersFrom(AppModule),
    { provide: 'BASE_URL', useFactory: getBaseUrl, deps: [] }
  ]
}).catch(err => console.error(err));