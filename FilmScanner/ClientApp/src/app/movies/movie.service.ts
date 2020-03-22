import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { IMovie as Movie } from './Movie';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { SearchResult } from './SearchResult';

@Injectable()
export class MoviesService {

  constructor(private http: HttpClient) {
  }

  searchMoviesByTitle(movieTitle: string, page: number = 1): Observable<SearchResult> {
    return this.http.get<SearchResult>(this.getUrlWithTitleAndPage(movieTitle, page));
  }

  private getUrlWithTitleAndPage(movieTitle: string, page: number) {
    return environment.apiUrl + '/?s=' + movieTitle + '&page=' + page + '&apikey=' + environment.apiKey;
  }

  searchMovieByID(movieID: string): Observable<Movie> {
    return this.http.get<Movie>(this.getUrlWithID(movieID));
  }

  private getUrlWithID(movieID: string) {
    return environment.apiUrl + '/?i=' + movieID + '&apikey=' + environment.apiKey;
  }
}
