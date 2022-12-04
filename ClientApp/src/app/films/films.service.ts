import { HttpClient } from '@angular/common/http';
import { Film } from './film';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { SearchResult } from './searchResult';
import { environment } from 'src/environments/environment';

@Injectable()
export class FilmsService {

  constructor(private http: HttpClient) {
  }

  searchFilmsByTitle(movieTitle: string, page: number = 1): Observable<SearchResult> {
    return this.http.get<SearchResult>(this.getUrlWithTitleAndPage(movieTitle, page));
  }

  private getUrlWithTitleAndPage(movieTitle: string, page: number): string {
    return environment.omdbApiAddress + '/search/' + movieTitle + '/' + page;
  }

  searchFilmByID(movieID: string): Observable<Film> {
    return this.http.get<Film>(this.getUrlWithID(movieID));
  }

  private getUrlWithID(movieID: string): string {
    return environment.omdbApiAddress + '/film/' + movieID;
  }
}
