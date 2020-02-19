import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { Observable } from 'rxjs';
import { IMovie } from './imovie';
import { environment } from '../../environments/environment';

@Injectable()
export class MoviesService {

  private _url: string;

  constructor(private http: HttpClient) {
  }

  searchMovies(value: string): Observable<IMovie[]> {
    return this.http.get<IMovie[]>(this.getUrl(value));
  }

  private getUrl(value: string){
    return environment.apiUrl + "/?s=" + value + "&apikey=" + environment.apiKey;
  }
}
