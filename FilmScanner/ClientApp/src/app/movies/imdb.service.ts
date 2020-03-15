import { environment } from '../../environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable()
export class ImdbService {

  constructor(private http: HttpClient) {
  }

  getMetaData(movieId: string): Observable<Object> {
    const headerDict = {
      'x-rapidapi-host': environment.imdbHost,
      'x-rapidapi-key': environment.imdbApiKey,
    };

    const requestOptions = {
      headers: new HttpHeaders(headerDict),
    };

    return this.http.get('https://' + environment.imdbHost + '/title/get-meta-data?region=US&ids=' + movieId, requestOptions);
  }
}

