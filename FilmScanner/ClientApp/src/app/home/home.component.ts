import { AppTitleService } from '../services/appTitle.service';
import { Component, OnInit } from '@angular/core';
import { FilmsService } from '../films/films.service';
import { Router } from '@angular/router';
import { SearchResult } from '../films/searchResult';
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
  filmsLoader = true;
  imageLoader = true;
  isPremieresChosen = true;
  isMostPopularChosen = false;
  isComediesChosen = false;
  isThrillersChosen = false;
  isActionChosen = false;
  films: SearchResult;
  sliceBottom = 5;
  sliceTop = 10;
  page = 1;
  category = 'spider-man';
  private _state = 'visible-center';

  public get shouldRightButtonAppear() {
    return (this.page == 1 && this.sliceTop == 10)
  }

  public get state() {
    return this._state;
  }
  public set state(value) {
    this._state = value;
  }

  constructor(private _appTitleService: AppTitleService, private _filmsService: FilmsService, private _router: Router) {
  }

  ngOnInit() {
    this._appTitleService.setTitle('Home');
    this.subscribeToSearchResult();
  }

  rotateCarousel(event: AnimationEvent) {
    if (event.toState === 'vanished-left') {
      this.leftArrowClick();
      this.state = 'vanished-right2';
    }

    if (event.toState === 'vanished-right') {
      this.rightArrowClick();
      this.state = 'vanished-left2';
    }

    if (event.toState === 'vanished-right2' || event.toState === 'vanished-left2') {
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
    this.sliceTop = 10;
    this.sliceBottom = 5;
  }

  resetAllBooleans() {
    this.isPremieresChosen = false;
    this.isMostPopularChosen = false;
    this.isComediesChosen = false;
    this.isThrillersChosen = false;
    this.isActionChosen = false;
  }

  selectfilm(imdbID: string) {
    this._router.navigate(['/film-overview'],
      { queryParams: { filmID: imdbID } });
  }

  rightArrowClick() {
    if (this.sliceTop === 5) {
      this.sliceBottom += 5;
      this.sliceTop += 5;
    } else {
      this.films = null;
      this.resetSlices();
      this.page--;
      this.subscribeToSearchResult();
    }
  }

  leftArrowClick() {
    if (this.sliceTop === 10) {
      this.sliceBottom -= 5;
      this.sliceTop -= 5;
    } else {
      this.films = null;
      this.sliceBottom = 5;
      this.sliceTop = 10;
      this.page++;
      this.subscribeToSearchResult();
    }
  }

  private subscribeToSearchResult() {
    this._filmsService.searchFilmsByTitle(this.category, this.page)
      .subscribe(data => this.films = data);
  }
}
