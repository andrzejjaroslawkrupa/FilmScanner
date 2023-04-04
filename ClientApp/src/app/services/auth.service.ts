import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { JwtHelperService } from '@auth0/angular-jwt';
import { environment } from 'src/environments/environment';

const AUTH_USER_STORAGE_KEY = environment.userProfileStorageKey;

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private _jwtHelper = new JwtHelperService();

  constructor(private _http: HttpClient) { }

  public login(username: string, password: string): Observable<any> {
    return this._http.post<any>(environment.authenticationEnpointAddress, { username, password });
  }

  private _getToken(): string {
    return JSON.parse(localStorage.getItem(AUTH_USER_STORAGE_KEY));
  }

  public isAuthenticated(): boolean {
    const token = this._getToken();
    return token && !this._jwtHelper.isTokenExpired(token);
  }

  public getAuthHeaders(): HttpHeaders {
    const token = this._getToken();
    if (this.isAuthenticated()) {
      return new HttpHeaders({
        'Authorization': `Bearer ${token}`,
        'Content-Type': 'application/json'
      });
    }
    return new HttpHeaders({ 'Content-Type': 'application/json' });
  }

  public saveAuthResponseToLocalStorage(response: any): void {
    localStorage.setItem(AUTH_USER_STORAGE_KEY, JSON.stringify(response));
  }

  public logout(): void {
    localStorage.removeItem(AUTH_USER_STORAGE_KEY);
  }
}
