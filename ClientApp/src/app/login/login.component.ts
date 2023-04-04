import { Component } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { FormControl, Validators } from '@angular/forms';
import { passwordValidator } from './password-validator';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.scss'],
})
export class LoginComponent {
    usernameFormControl = new FormControl('', [Validators.required]);
    passwordFormControl = new FormControl('', [Validators.required, passwordValidator()]);
    showLoggedIn: boolean = false;

    constructor(private _authService: AuthService) { }

    public submit(): void {
        this._authService.login(this.usernameFormControl.value, this.passwordFormControl.value).subscribe(
            (response) => {
                this._authService.saveAuthResponseToLocalStorage(response.token);
                this.showLoggedIn = true;
            },
            (error) => {
                this.showLoggedIn = false;
                console.error(error);
            }
        );
    }
}
