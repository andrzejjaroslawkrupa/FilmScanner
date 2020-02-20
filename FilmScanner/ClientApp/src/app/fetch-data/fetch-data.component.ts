import { AfterViewInit, Component, ElementRef, OnDestroy, ViewChild } from '@angular/core';
import { MoviesService } from './movie-service';
import { AutofillMonitor } from '@angular/cdk/text-field';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent implements AfterViewInit, OnDestroy {
  @ViewChild('search', {static: true}) searchText: ElementRef<HTMLElement>;
  searchTextAutofilled: boolean;
  public movies = [];

  constructor(private _moviesService: MoviesService, private _autofill: AutofillMonitor) {
  }

  ngAfterViewInit() {
    this._autofill.monitor(this.searchText)
        .subscribe(e => this.searchTextAutofilled = e.isAutofilled);
  }

  OnInput(value: string) {
    this._moviesService.searchMovies(value.trim())
      .subscribe(data => this.movies = data);
   }

  ngOnDestroy() {
    this._autofill.stopMonitoring(this.searchText);
  }
}
