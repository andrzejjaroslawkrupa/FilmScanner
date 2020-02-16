import {Component, OnInit} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {MoviesService} from './movie-service';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public movies = [];

  constructor(private _moviesService: MoviesService) {
  }

  ngOnInit() {
    this._moviesService.getMovies()
      .subscribe(data => this.movies = data);
  }
}
