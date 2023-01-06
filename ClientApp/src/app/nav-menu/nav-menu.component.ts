import { Component, ElementRef, ViewChild } from '@angular/core';
import { FilmsService } from '../films/films.service';
import { Router } from '@angular/router';
import { SearchResult } from '../films/searchResult';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.scss']
})
export class NavMenuComponent {
  @ViewChild('search', { static: true }) searchText: ElementRef<HTMLElement>;
  public films: SearchResult;
  isExpanded = false;

  constructor(private _filmsService: FilmsService, private router: Router) {
  }

  onInput(value: string) {
    this._filmsService.searchFilmsByTitle(value.trim())
      .subscribe(data => this.films = data);
  }

  selectFilm(imdbID: string) {
    this.router.navigate(['/film-overview'],
      { queryParams: { filmID: imdbID } });
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  reset() {
    if (this.router.url === '/') {
      window.location.reload();
    }
    this.router.navigate(['/']);
  }
}
