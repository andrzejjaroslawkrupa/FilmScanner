import { Component, signal } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { FormControl, Validators, ReactiveFormsModule } from '@angular/forms';
import { passwordValidator } from '../validators/password.validator';
import { Router } from '@angular/router';

import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

@Component({
    selector: 'app-login',
    standalone: true,
    imports: [ReactiveFormsModule, MatFormFieldModule, MatInputModule, MatButtonModule, MatProgressSpinnerModule],
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.scss'],
})
export class LoginComponent {
    usernameFormControl = new FormControl('', [Validators.required]);
    passwordFormControl = new FormControl('', [Validators.required, passwordValidator()]);
    showLoggedIn = signal(false);
    errorOccured = signal(false);
    loggingIn = signal(false);

    constructor(private _authService: AuthService, private _router: Router) { }

    public submit(): void {
    this.loggingIn.set(true);
    this._authService.login(this.usernameFormControl.value ?? '', this.passwordFormControl.value ?? '').subscribe(
            (response) => {
                this._authService.saveAuthResponseToLocalStorage(response.token);
                this.showLoggedIn.set(true);
                this.loggingIn.set(false);
                this._router.navigate(['/']);
            },
            (error) => {
                this.showLoggedIn.set(false);
                this.errorOccured.set(true);
                console.error(error);
                this.loggingIn.set(false);
            }
        );
    }
}
