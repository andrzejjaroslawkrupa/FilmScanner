import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MoviesService } from '../movies/movie.service';

@Component({
  selector: 'app-movie-overview',
  templateUrl: './movie-overview.component.html',
  styleUrls: ['./movie-overview.component.css']
})
export class MovieOverviewComponent implements OnInit {

  constructor(private route: ActivatedRoute, private _moviesService: MoviesService) { }

  movieID: string;
  public movie;

  ngOnInit() {
    this.movieID = this.route.snapshot.queryParamMap.get("movieID");
     this._moviesService.searchMovieByID(this.movieID)
      .subscribe(data => {
        if (data)
          this.movie = data
      });
  }

  test() {

  }
}
