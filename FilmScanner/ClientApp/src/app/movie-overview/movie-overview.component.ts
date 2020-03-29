import { ActivatedRoute } from '@angular/router';
import { AppTitleService } from '../services/appTitle.service';
import { Component, OnInit } from '@angular/core';
import { ImdbService } from '../films/imdb.service';
import { Film } from '../films/film';
import { FilmsService } from '../films/films.service';

@Component({
  selector: 'app-movie-overview',
  templateUrl: './movie-overview.component.html',
  styleUrls: ['./movie-overview.component.css']
})
export class MovieOverviewComponent implements OnInit {
  imageLoader = true;
  movieLoader = true;

  constructor(
    private _appTitleService: AppTitleService,
    private _imdbService: ImdbService,
    private _route: ActivatedRoute,
    private _filmsService: FilmsService
  ) { }

  movieID: string;
  public movie: Film;
  public waysToWatch = [];

  ngOnInit() {
    this._route.queryParams.subscribe(params => {
      this._filmsService.searchMovieByID(params['movieID'])
        .subscribe(data => {
          if (data) {
            this.movie = data;
            this._appTitleService.setTitle(this.movie.title);
          }
        });
      this.movieID = params['movieID'];
    });

    this._imdbService.getMetaData(this.movieID).subscribe(result =>
      this.waysToWatch = result[this.movieID].waysToWatch.optionGroups[0].watchOptions);
  }

  addToCollection() {
    console.log(this.movieID);
  }
}
