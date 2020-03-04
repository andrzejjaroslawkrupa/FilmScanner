import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-movie-overview',
  templateUrl: './movie-overview.component.html',
  styleUrls: ['./movie-overview.component.css']
})
export class MovieOverviewComponent implements OnInit {

  constructor(private route: ActivatedRoute) { }

  movieID: string;

  ngOnInit() {
    this.movieID = this.route.snapshot.queryParamMap.get("movieID")
  }
}
