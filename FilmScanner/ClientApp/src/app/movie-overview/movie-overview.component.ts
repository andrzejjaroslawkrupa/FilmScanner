import { ActivatedRoute } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { ImdbService } from '../movies/imdb.service';
import { IMovie } from '../movies/Movie';
import { MoviesService } from '../movies/movie.service';

@Component({
  selector: 'app-movie-overview',
  templateUrl: './movie-overview.component.html',
  styleUrls: ['./movie-overview.component.css']
})
export class MovieOverviewComponent implements OnInit {
  imageLoader = true;

  constructor(
    private route: ActivatedRoute,
    private _imdbService: ImdbService,
    private _moviesService: MoviesService
    ) { }

  movieID: string;
  public movie: IMovie;
  public waysToWatch = [];

  ngOnInit() {
    this.route.queryParams.subscribe(params => {
      this._moviesService.searchMovieByID(params['movieID'])
      .subscribe(data => {
        if (data) {
          this.movie = data;
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
