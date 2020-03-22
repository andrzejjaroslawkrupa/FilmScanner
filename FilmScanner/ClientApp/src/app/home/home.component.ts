import { Component, OnInit } from '@angular/core';
import { MoviesService } from '../movies/movie.service';
import { Router } from '@angular/router';
import { SearchResult } from '../movies/SearchResult';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  imageLoader = true;
  isPremieresChosen = true;
  isMostPopularChosen = false;
  isComediesChosen = false;
  isThrillersChosen = false;
  isActionChosen = false;
  movies: SearchResult;
  sliceBottom = 0;
  sliceTop = 5;
  page = 1;
  category = 'spider-man';

  constructor(private _moviesService: MoviesService, private router: Router) {
  }

  ngOnInit() {
    this.subscribeToSearchResult();
  }

  changeCategory(category: string) {
    this.resetAllBooleans();
    this.category = category;
    this.page = 1;
    this.resetSlices();
    this.subscribeToSearchResult();
  }

  resetSlices() {
    this.sliceTop = 5;
    this.sliceBottom = 0;
  }

  resetAllBooleans() {
    this.isPremieresChosen = false;
    this.isMostPopularChosen = false;
    this.isComediesChosen = false;
    this.isThrillersChosen = false;
    this.isActionChosen = false;
  }

  selectMovie(imdbID: string) {
    this.router.navigate(['/movie-overview'],
      { queryParams: { movieID: imdbID } });
  }

  leftArrowClick() {
    if (this.sliceTop === 5) {
      this.sliceBottom += 5;
      this.sliceTop += 5;
    } else {
      this.movies = null;
      this.resetSlices();
      this.page++;
      this.subscribeToSearchResult();
    }
  }

  rightArrowClick() {
    if ( this.page === 1 && this.sliceTop === 5) {
      return;
    }
    if (this.sliceTop === 10) {
      this.sliceBottom -= 5;
      this.sliceTop -= 5;
    } else {
      this.movies = null;
      this.sliceBottom = 5;
      this.sliceTop = 10;
      this.page--;
      this.subscribeToSearchResult();
    }
  }

  private subscribeToSearchResult() {
    this._moviesService.searchMoviesByTitle(this.category, this.page)
      .subscribe(data => this.movies = data);
  }
}
