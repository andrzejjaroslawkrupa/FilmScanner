import { ActivatedRoute } from '@angular/router';
import { AppTitleService } from '../services/appTitle.service';
import { Component, OnInit } from '@angular/core';
import { ImdbService } from '../movies/imdb.service';
import { IMovie } from '../movies/Movie';
import { MoviesService } from '../movies/movie.service';
import { Title } from '@angular/platform-browser';

@Component({
  selector: 'app-movie-overview',
  templateUrl: './movie-overview.component.html',
  styleUrls: ['./movie-overview.component.css']
})
export class MovieOverviewComponent implements OnInit {
  imageLoader = true;

  constructor(
    private _appTitleService: AppTitleService,
    private _imdbService: ImdbService,
    private _route: ActivatedRoute,
    private _moviesService: MoviesService
  ) { }

  movieID: string;
  public movie: IMovie;
  public waysToWatch = [];

  ngOnInit() {
    this._route.queryParams.subscribe(params => {
      this._moviesService.searchMovieByID(params['movieID'])
        .subscribe(data => {
          if (data) {
            this.movie = data;
            this._appTitleService.setTitle(this.movie.Title);
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
