import { ActivatedRoute } from '@angular/router';
import { AppTitleService } from '../services/appTitle.service';
import { Component, OnInit, signal } from '@angular/core';

import { Film } from '../films/film';
import { FilmsService } from '../services/films.service';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

@Component({
    selector: 'app-film-overview',
    standalone: true,
    imports: [MatProgressSpinnerModule],
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

    filmID: string | undefined;
    public film = signal<Film | undefined>(undefined);

    ngOnInit() {
        this._route.queryParams.subscribe(params => {
            this._filmsService.searchFilmByID(params['filmID'])
                .subscribe(data => {
                    if (data) {
                        this.film.set(data);
                        this._appTitleService.setTitle(data.title);
                    }
                });
            this.filmID = params['filmID'];
        });
    }
}