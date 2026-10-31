import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

// Mirrors the backend's ASP.NET Identity default password options (FilmScanner.Web ConfigureIdentity):
// RequiredLength = 6, RequireUppercase, RequireLowercase, RequireDigit, RequireNonAlphanumeric.
// Identity only counts ASCII A-Z, a-z and 0-9 as letters/digits, so any other character is "non-alphanumeric".
export const PASSWORD_MIN_LENGTH = 6;

export function passwordValidator(): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const value: string = control.value ?? '';
    const valid = value.length >= PASSWORD_MIN_LENGTH
      && /[A-Z]/.test(value)
      && /[a-z]/.test(value)
      && /[0-9]/.test(value)
      && /[^A-Za-z0-9]/.test(value);

    return valid ? null : { 'passwordStrength': true };
  };
}

export const passwordMatchValidator: ValidatorFn = (control: AbstractControl): ValidationErrors | null => {
  const password = control.get('password');
  const repeatPassword = control.get('repeatPassword');

  if (password && repeatPassword && password.value !== repeatPassword.value) {
    return { passwordMismatch: true };
  }

  return null;
};