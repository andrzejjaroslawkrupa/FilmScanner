import { Component, ElementRef, ViewChild } from '@angular/core';
import { MoviesService } from '../movies/movie.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent{
  @ViewChild('search', {static: true}) searchText: ElementRef<HTMLElement>;
  public movies = [];
  isExpanded = false;

  constructor(private _moviesService: MoviesService) {
  }

  OnInput(value: string) {
    this._moviesService.searchMovies(value.trim())
      .subscribe(data => this.movies = data);
   }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
