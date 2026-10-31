import { Component, ElementRef, ViewChild } from '@angular/core';

import { FormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { RouterLink } from '@angular/router';
import { FilmsService } from '../services/films.service';
import { Router } from '@angular/router';
import { SearchResult } from '../films/searchResult';
import { AuthService } from '../services/auth.service';
import { MatMenuModule } from '@angular/material/menu';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-nav-menu',
  standalone: true,
  imports: [FormsModule, MatFormFieldModule, MatInputModule, MatAutocompleteModule, MatMenuModule, MatButtonModule, RouterLink],
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.scss']
})
export class NavMenuComponent {
  @ViewChild('search', { static: true }) searchText!: ElementRef<HTMLElement>;
public films: SearchResult | undefined;
  isExpanded = false;

  constructor(private _filmsService: FilmsService, private _router: Router, private _authService: AuthService) {
  }

  onInput(value: string): void {
    this._filmsService.searchFilmsByTitle(value.trim())
      .subscribe(data => this.films = data);
  }

  selectFilm(imdbID: string): void {
    this._router.navigate(['/film-overview'],
      { queryParams: { filmID: imdbID } });
  }

  home(): void {
    if (this._router.url === '/') {
      window.location.reload();
    }
    this._router.navigate(['/']);
  }

  isLoggedIn(): boolean {
    return this._authService.isAuthenticated();
  }

  logout(): void {
    this._authService.logout();
    if (this._router.url === '/profile'){
      this._router.navigate(['/']);
    }
  }
}
