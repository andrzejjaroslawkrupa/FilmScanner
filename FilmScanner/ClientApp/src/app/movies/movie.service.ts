import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { Observable } from 'rxjs';
import { IMovie } from './imovie';
import { environment } from '../../environments/environment';

@Injectable()
export class MoviesService {

  constructor(private http: HttpClient) {
  }

  searchMoviesByTitle(movieTitle: string, page: number = 1): Observable<IMovie[]> {
    return this.http.get<IMovie[]>(this.getUrlWithTitleAndPage(movieTitle, page));
  }

  private getUrlWithTitleAndPage(movieTitle: string, page: number) {
    return environment.apiUrl + "/?s=" + movieTitle + "&page="+ page +"&apikey=" + environment.apiKey;
  }

  searchMovieByID(movieID: string): Observable<IMovie> {
    return this.http.get<IMovie>(this.getUrlWithID(movieID));
  }

  private getUrlWithID(movieID: string) {
    return environment.apiUrl + "/?i=" + movieID + "&apikey=" + environment.apiKey;
  }
}
