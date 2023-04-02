import { Component } from '@angular/core';
import { AuthService } from '../services/auth.service';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.scss'],
})
export class LoginComponent {
    username: string = "";
    password: string = "";
    show: boolean = false;

    constructor(private _authService: AuthService) { }

    public submit(): void {
        this._login();
        this._clear();
    }

    private _login(): void {
        this._authService.login(this.username, this.password).subscribe(
            (response) => {
                this._authService.saveAuthResponseToLocalStorage(response.token);
            },
            (error) => {
                console.error(error);
            })
    }

    private _clear(): void {
        this.show = true;
    }
}
