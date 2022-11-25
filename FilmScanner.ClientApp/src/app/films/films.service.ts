import { HttpClient } from '@angular/common/http';
import { Film } from './film';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { SearchResult } from './searchResult';

@Injectable()
export class FilmsService {

  constructor(private http: HttpClient) {
  }

  searchFilmsByTitle(movieTitle: string, page: number = 1): Observable<SearchResult> {
    return this.http.get<SearchResult>(this.getUrlWithTitleAndPage(movieTitle, page));
  }

  private getUrlWithTitleAndPage(movieTitle: string, page: number): string {
    return 'http://localhost:5000/api/externalfilms/search/' + movieTitle + '/' + page;
  }

  searchFilmByID(movieID: string): Observable<Film> {
    return this.http.get<Film>(this.getUrlWithID(movieID));
  }

  private getUrlWithID(movieID: string): string {
    return 'http://localhost:5000/api/externalfilms/film/' + movieID;
  }
}
