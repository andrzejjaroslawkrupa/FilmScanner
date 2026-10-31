import { Component, ElementRef, ViewChild, signal } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { Subject, of } from 'rxjs';
import { catchError, debounceTime, distinctUntilChanged, map, switchMap } from 'rxjs/operators';

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
  public films = signal<SearchResult | undefined>(undefined);
  isExpanded = false;

  static readonly SEARCH_DEBOUNCE_MS = 300;
  private readonly _searchTerms = new Subject<string>();

  constructor(private _filmsService: FilmsService, private _router: Router, private _authService: AuthService) {
    this._searchTerms.pipe(
      map(term => term.trim()),
      debounceTime(NavMenuComponent.SEARCH_DEBOUNCE_MS),
      distinctUntilChanged(),
      // switchMap cancels the in-flight request when a newer term arrives,
      // so a late response for an older prefix can't overwrite newer results.
      switchMap(term => term
        ? this._filmsService.searchFilmsByTitle(term).pipe(catchError(() => of(undefined)))
        : of(undefined)),
      takeUntilDestroyed(),
    ).subscribe(result => this.films.set(result));
  }

  onInput(value: string): void {
    this._searchTerms.next(value);
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
