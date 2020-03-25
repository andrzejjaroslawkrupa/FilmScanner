import { AppTitleService } from '../services/appTitle.service';
import { Component, OnInit } from '@angular/core';
import { MoviesService } from '../movies/movie.service';
import { Router } from '@angular/router';
import { SearchResult } from '../movies/SearchResult';
import { trigger, state, style, transition, animate, AnimationEvent } from '@angular/animations';

@Component({

  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
  animations: [
    trigger('transform', [
      state('visible-center', style({
        opacity: 1,
        marginLeft: 0,
        marginRight: 0
      })),
      state('vanished-left', style({
        opacity: 0,
        marginLeft: '20vw',
        marginRight: '-20vw'
      })),
      state('vanished-left2', style({
        opacity: 0,
        marginLeft: '20vw',
        marginRight: '-20vw'
      })),
      state('vanished-right', style({
        opacity: 0,
        marginLeft: '-20vw',
        marginRight: '20vw'
      })),
      state('vanished-right2', style({
        opacity: 0,
        marginLeft: '-20vw',
        marginRight: '20vw'
      })),
      transition('* <=> void', [
        animate('0s')
      ]),
      transition('* <=> *', [
        animate('0.6s')
      ]),
    ]),
  ],
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
  private _state = 'visible-center';

  public get state() {
    return this._state;
  }
  public set state(value) {
    this._state = value;
  }

  constructor(private _appTitleService: AppTitleService, private _moviesService: MoviesService, private _router: Router) {
  }

  ngOnInit() {
    this._appTitleService.setTitle('Home');
    this.subscribeToSearchResult();
  }

  rotateCarousel(event: AnimationEvent) {
    if (event.toState == 'vanished-left') {
      this.leftArrowClick();
      this.state = 'vanished-right2';
    }

    if (event.toState == 'vanished-right') {
      this.rightArrowClick();
      this.state = 'vanished-left2';
    }

    if (event.toState == 'vanished-right2' || event.toState == 'vanished-left2') {
      this.state = 'visible-center';
    }
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
    this._router.navigate(['/movie-overview'],
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
    if (this.page === 1 && this.sliceTop === 5) {
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
