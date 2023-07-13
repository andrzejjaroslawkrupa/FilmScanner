import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Register } from "../registration/register";
import { environment } from "src/environments/environment";
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class RegisterService {
    private _registerPath: string = "/users/register";

    constructor(private _http: HttpClient) { }

    public register(user: Register): Observable<any> {
        return this._http.post<Register>(environment.filmscannerApiAdress + this._registerPath, user);
    }
}