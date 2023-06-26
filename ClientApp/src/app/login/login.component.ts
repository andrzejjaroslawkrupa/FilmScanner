import { Component } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { FormControl, Validators } from '@angular/forms';
import { passwordValidator } from '../validators/password.validator';
import { Router } from '@angular/router';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.scss'],
})
export class LoginComponent {
    usernameFormControl = new FormControl('', [Validators.required]);
    passwordFormControl = new FormControl('', [Validators.required, passwordValidator()]);
    showLoggedIn: boolean = false;
    errorOccured: boolean = false;
    loggingIn: boolean = false;

    constructor(private _authService: AuthService, private _router: Router) { }

    public submit(): void {
        this.loggingIn = true;
        this._authService.login(this.usernameFormControl.value, this.passwordFormControl.value).subscribe(
            (response) => {
                this._authService.saveAuthResponseToLocalStorage(response.token);
                this.showLoggedIn = true;
                this.loggingIn = false;
                this._router.navigate(['/']);
            },
            (error) => {
                this.showLoggedIn = false;
                this.errorOccured = true;
                console.error(error);
                this.loggingIn = false;
            }
        );
    }
}
