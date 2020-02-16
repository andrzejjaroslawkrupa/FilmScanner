import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { Observable } from 'rxjs';
import { IMovie } from './imovie';
import { environment } from '../../environments/environment';

@Injectable()
export class MoviesService {

  private _url: string = environment.apiUrl + "/?s=batman&apikey=" + environment.apiKey;

  constructor(private http: HttpClient) {
  }

  getMovies(): Observable<IMovie[]> {
    return this.http.get<IMovie[]>(this._url);
  }
}
