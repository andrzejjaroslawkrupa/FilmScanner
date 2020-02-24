import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { Observable } from 'rxjs';
import { IMovie } from './imovie';
import { environment } from '../../environments/environment';

@Injectable()
export class MoviesService {

  constructor(private http: HttpClient) {
  }

  searchMovies(movieTitle: string): Observable<IMovie[]> {
    return this.http.get<IMovie[]>(this.getUrl(movieTitle));
  }

  private getUrl(movieTitle: string){
    return environment.apiUrl + "/?s=" + movieTitle + "&apikey=" + environment.apiKey;
  }
}
