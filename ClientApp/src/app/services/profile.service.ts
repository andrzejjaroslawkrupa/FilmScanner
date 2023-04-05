import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "src/environments/environment";
import { Profile } from "../profile/profile";
import { AuthService } from "./auth.service";

@Injectable()
export class ProfileService {
    constructor(private _http: HttpClient, private _authService: AuthService) { }

    public getProfile(): Observable<Profile> {
        const httpOptions = {
            headers: this._authService.getAuthHeaders()
        };
        return this._http.get<Profile>(environment.filmscannerApiAdress + '/profile/profile', httpOptions);
    }
}