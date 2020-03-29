import { Component, ElementRef, ViewChild } from '@angular/core';
import { FilmsService } from '../films/films.service';
import { Router } from '@angular/router';
import { SearchResult } from '../films/SearchResult';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  @ViewChild('search', { static: true }) searchText: ElementRef<HTMLElement>;
  public movies: SearchResult;
  isExpanded = false;

  constructor(private _filmsService: FilmsService, private router: Router) {
  }

  onInput(value: string) {
    this._filmsService.searchMoviesByTitle(value.trim())
      .subscribe(data => this.movies = data);
  }

  selectMovie(imdbID: string) {
    this.router.navigate(['/movie-overview'],
      { queryParams: { movieID: imdbID } });
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
