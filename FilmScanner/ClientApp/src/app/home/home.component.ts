import { AppTitleService } from '../services/appTitle.service';
import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { FilmsService } from '../films/films.service';
import { Router } from '@angular/router';
import { SearchResult } from '../films/searchResult';
import { Slide } from '../carousel/carousel.interface';
import { debounceTime, distinctUntilChanged } from 'rxjs/operators';

@Component({

  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  @ViewChild('carousel', { static: false }) carousel: ElementRef;

  filmsLoader = true;
  imageLoader = true;
  isPremieresChosen = true;
  isMostPopularChosen = false;
  isComediesChosen = false;
  isThrillersChosen = false;
  isActionChosen = false;

  searchResultsPageNumber = 1;
  category = 'spider-man';

  slides: Slide[] = [];
  carouselsCurrentPage = 0;

  constructor(private _appTitleService: AppTitleService, private _filmsService: FilmsService, private _router: Router) {
  }

  ngOnInit(): void {
    this._appTitleService.setTitle('Home');
    this.subscribeToSearchResult();
  }

  changeCategory(category: string): void {
    this.resetAllBooleans();
    this.category = category;
    this.searchResultsPageNumber = 1;
    this.slides = [];
    this.carouselsCurrentPage = 0;
    this.subscribeToSearchResult();
  }

  resetAllBooleans(): void {
    this.isPremieresChosen = false;
    this.isMostPopularChosen = false;
    this.isComediesChosen = false;
    this.isThrillersChosen = false;
    this.isActionChosen = false;
  }

  private subscribeToSearchResult(): void {
    this._filmsService.searchFilmsByTitle(this.category, this.searchResultsPageNumber).pipe(
      debounceTime(400),
      distinctUntilChanged()
    ).subscribe(data => this.convertDataToSlides(data));
  }

  private convertDataToSlides(data: SearchResult): void {
    data.searches.forEach(film => {
      this.slides.push({
        title: film.title + ' (' + film.year + ')',
        imageSrc: this.ensureCarouselGetsImage(film.poster),
        destination: 'film-overview',
        parameterKey: 'filmID',
        parameterValue: film.imdbID
      });
    });
  }

  private ensureCarouselGetsImage(posterLink: string): string {
    if (posterLink === 'N/A') {
      return 'assets/camera.png';
    }

    return posterLink;
  }

  checkIfDataIsRunningOut(isNextLast: boolean): void {
    if (isNextLast) {
      this.searchResultsPageNumber++;
      this.subscribeToSearchResult();
    }
  }
}
