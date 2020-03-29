import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Film } from './film';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { SearchResult } from './SearchResult';

@Injectable()
export class FilmsService {

  constructor(private http: HttpClient) {
  }

  searchMoviesByTitle(movieTitle: string, page: number = 1): Observable<SearchResult> {
    return this.http.get<SearchResult>(this.getUrlWithTitleAndPage(movieTitle, page));
  }

  private getUrlWithTitleAndPage(movieTitle: string, page: number) {
    return 'api/externalfilms/search/' + movieTitle + '/' + page;
  }

  searchMovieByID(movieID: string): Observable<Film> {
    return this.http.get<Film>(this.getUrlWithID(movieID));
  }

  private getUrlWithID(movieID: string) {
    return 'api/externalfilms/film/' + movieID;
  }
}
