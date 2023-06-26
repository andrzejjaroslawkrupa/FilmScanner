import { AppComponent } from './app.component';
import { AppTitleService } from './services/appTitle.service';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HomeComponent } from './home/home.component';
import { HttpClientModule } from '@angular/common/http';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { FilmOverviewComponent } from './film-overview/film-overview.component';
import { FilmsService } from './services/films.service';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NgModule } from '@angular/core';
import { OverlayModule } from '@angular/cdk/overlay';
import { ProfileComponent } from './profile/profile.component';
import { RouterModule } from '@angular/router';
import { TextFieldModule } from '@angular/cdk/text-field';
import { CarouselComponent } from './carousel/carousel.component';
import { LoginComponent } from './login/login.component';
import { MatButtonModule } from '@angular/material/button';
import { ProfileService } from './services/profile.service';
import { MatMenuModule } from '@angular/material/menu';
import { RegistrationComponent } from './registration/registration.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    FilmOverviewComponent,
    NavMenuComponent,
    ProfileComponent,
    CarouselComponent,
    LoginComponent,
    RegistrationComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'profile', component: ProfileComponent },
      { path: 'film-overview', component: FilmOverviewComponent },
      { path: 'login', component: LoginComponent },
      { path: 'registration', component: RegistrationComponent },
    ], {}),
    BrowserAnimationsModule,
    TextFieldModule,
    MatFormFieldModule,
    MatInputModule,
    MatAutocompleteModule,
    OverlayModule,
    ReactiveFormsModule,
    MatProgressSpinnerModule,
    NgbModule,
    MatButtonModule,
    MatMenuModule,
  ],
  providers: [AppTitleService, FilmsService, ProfileService,],
  bootstrap: [AppComponent],
})
export class AppModule { }
