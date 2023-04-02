import { ActivatedRoute } from '@angular/router';
import { AppTitleService } from '../services/appTitle.service';
import { Component, OnInit } from '@angular/core';
import { Film } from '../films/film';
import { FilmsService } from '../services/films.service';

@Component({
  selector: 'app-film-overview',
  templateUrl: './film-overview.component.html',
  styleUrls: ['./film-overview.component.scss']
})
export class FilmOverviewComponent implements OnInit {
  imageLoader = true;
  filmLoader = true;

  constructor(
    private _appTitleService: AppTitleService,
    private _route: ActivatedRoute,
    private _filmsService: FilmsService
  ) { }

  filmID: string;
  public film: Film;

  ngOnInit() {
    this._route.queryParams.subscribe(params => {
      this._filmsService.searchFilmByID(params['filmID'])
        .subscribe(data => {
          if (data) {
            this.film = data;
            this._appTitleService.setTitle(this.film.title);
          }
        });
      this.filmID = params['filmID'];
    });
  }
}
