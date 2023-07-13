import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { passwordMatchValidator, passwordValidator } from '../validators/password.validator';
import { RegisterService } from '../services/register.service';
import { Register } from './register';
import { Router } from '@angular/router';

@Component({
    selector: 'app-registration',
    templateUrl: './registration.component.html',
    styleUrls: ['./registration.component.scss'],
})
export class RegistrationComponent {
    usernameFormControl = new FormControl('', [Validators.required]);
    emailFormControl = new FormControl('', [Validators.required]);
    passwordFormControl = new FormControl('', [Validators.required, passwordValidator()]);
    repeatPasswordFormControl = new FormControl('', [Validators.required, passwordValidator()]);

    registrationForm = new FormGroup({
        password: this.passwordFormControl,
        repeatPassword: this.repeatPasswordFormControl
      }, { validators: passwordMatchValidator });

    showRegistered: boolean = false;
    errorOccured: boolean = false;
    registering: boolean = false;

    constructor(private _registerService: RegisterService, private _router: Router) { }

    public submit(): void {
        var user: Register = {
            email: this.emailFormControl.value,
            userName: this.usernameFormControl.value,
            password: this.passwordFormControl.value
        }

        this.registering = true;
        this._registerService.register(user).subscribe(
            () => {
                this.showRegistered = true;
                this.registering = false;
                this._router.navigate(['/']);
            },
            (error) => {
                this.showRegistered = false;
                this.errorOccured = true;
                console.error(error);
                this.registering = false;
            }
        );
    }
}
