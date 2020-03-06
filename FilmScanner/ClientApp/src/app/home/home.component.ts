import { Component, OnInit } from '@angular/core';
import { MoviesService } from '../movies/movie.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  isPremieresChosen: boolean = true;
  isMostPopularChosen: boolean = false;
  isComediesChosen: boolean = false;
  isThrillersChosen: boolean = false;
  isActionChosen: boolean = false;
  movies = [];
  sliceBottom: number = 0;
  sliceTop: number = 5;
  page: number = 1;
  category: string = "spider-man"

  constructor(private _moviesService: MoviesService) {
  }

  ngOnInit() {
    this.subscribeToSearchResult();
  }

  changeCategory(category: string){
    this.resetAllBooleans();
    this.category = category;
    this.resetPageAndSlices();
    this.subscribeToSearchResult();
  }

  resetPageAndSlices(){
    this.page = 1;
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

  leftArrowClick() {
    if(this.sliceTop == 5){
      this.sliceBottom += 5;
      this.sliceTop += 5;
    }
    else{
      this.sliceBottom = 0;
      this.sliceTop = 5;
      this.page++;
      this.subscribeToSearchResult();
    }
  }

  rightArrowClick() {
    if( this.page == 1 && this.sliceTop == 5){
      return;
    }
    if(this.sliceTop == 10){
      this.sliceBottom -= 5;
      this.sliceTop -= 5;
    }
    else{
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
