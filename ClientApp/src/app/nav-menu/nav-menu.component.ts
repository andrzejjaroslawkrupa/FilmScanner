import { Component, ElementRef, ViewChild } from '@angular/core';
import { FilmsService } from '../services/films.service';
import { Router } from '@angular/router';
import { SearchResult } from '../films/searchResult';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.scss']
})
export class NavMenuComponent {
  @ViewChild('search', { static: true }) searchText: ElementRef<HTMLElement>;
  public films: SearchResult;
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

  reset(): void {
    if (this._router.url === '/') {
      window.location.reload();
    }
    this._router.navigate(['/']);
  }

  isLoggedIn(): boolean {
    return this._authService.isAuthenticated();
  }
}
