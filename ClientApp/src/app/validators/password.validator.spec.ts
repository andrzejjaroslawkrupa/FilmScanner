import { FormControl, FormGroup } from '@angular/forms';

import { passwordMatchValidator, passwordValidator } from './password.validator';

describe('passwordValidator', () => {
  const validate = (value: string | null) => passwordValidator()(new FormControl(value));
  const strengthError = { passwordStrength: true };

  it('accepts a password meeting all of the backend Identity rules', () => {
    expect(validate('Smoke1!')).toBeNull();
    expect(validate('Abcdef12#')).toBeNull();
  });

  it('accepts exactly the minimum length of 6', () => {
    expect(validate('Ab1!cd')).toBeNull();
  });

  it('rejects a password without a non-alphanumeric character (previously accepted)', () => {
    expect(validate('Smoke1pass')).toEqual(strengthError);
  });

  it('rejects a password shorter than 6 characters', () => {
    expect(validate('Ab1!c')).toEqual(strengthError);
  });

  it('rejects a password without an uppercase letter', () => {
    expect(validate('smoke1!')).toEqual(strengthError);
  });

  it('rejects a password without a lowercase letter', () => {
    expect(validate('SMOKE1!')).toEqual(strengthError);
  });

  it('rejects a password without a digit', () => {
    expect(validate('Smoke!!')).toEqual(strengthError);
  });

  it('counts non-ASCII letters as special characters, like Identity does', () => {
    expect(validate('Zażółć1')).toBeNull();
  });

  it('treats empty and null values as invalid without throwing', () => {
    expect(validate('')).toEqual(strengthError);
    expect(validate(null)).toEqual(strengthError);
  });
});

describe('passwordMatchValidator', () => {
  const form = (password: string, repeatPassword: string) =>
    new FormGroup({ password: new FormControl(password), repeatPassword: new FormControl(repeatPassword) });

  it('reports a mismatch when passwords differ', () => {
    expect(passwordMatchValidator(form('Smoke1!', 'Smoke2!'))).toEqual({ passwordMismatch: true });
  });

  it('passes when passwords match', () => {
    expect(passwordMatchValidator(form('Smoke1!', 'Smoke1!'))).toBeNull();
  });
});
