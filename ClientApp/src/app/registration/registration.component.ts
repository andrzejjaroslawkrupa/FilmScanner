import { Component, signal } from '@angular/core';
import { FormControl, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { passwordMatchValidator, passwordValidator } from '../validators/password.validator';
import { RegisterService } from '../services/register.service';
import { Register } from './register';
import { Router } from '@angular/router';
import { registrationErrorMessages } from './registration-errors';

import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

@Component({
    selector: 'app-registration',
    standalone: true,
    imports: [ReactiveFormsModule, MatFormFieldModule, MatInputModule, MatButtonModule, MatProgressSpinnerModule],
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

    showRegistered = signal(false);
    errorOccured = signal(false);
    errorMessages = signal<string[]>([]);
    registering = signal(false);

    constructor(private _registerService: RegisterService, private _router: Router) { }

    public submit(): void {
        var user: Register = {
            email: this.emailFormControl.value ?? '',
            userName: this.usernameFormControl.value ?? '',
            password: this.passwordFormControl.value ?? ''
        }

        this.registering.set(true);
        this.errorOccured.set(false);
        this.errorMessages.set([]);
        this._registerService.register(user).subscribe(
            () => {
                this.showRegistered.set(true);
                this.registering.set(false);
                this._router.navigate(['/']);
            },
            (error) => {
                this.showRegistered.set(false);
                this.errorMessages.set(registrationErrorMessages(error));
                this.errorOccured.set(true);
                console.error(error);
                this.registering.set(false);
            }
        );
    }
}