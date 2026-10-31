import { ActivatedRoute } from '@angular/router';
import { AppTitleService } from '../services/appTitle.service';
import { Component, OnInit, ChangeDetectionStrategy } from '@angular/core';

import { Film } from '../films/film';
import { FilmsService } from '../services/films.service';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

@Component({
    selector: 'app-film-overview',
    standalone: true,
    imports: [MatProgressSpinnerModule],
    templateUrl: './film-overview.component.html',
    changeDetection: ChangeDetectionStrategy.Eager,
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
    public film: Film | undefined;

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