import { HttpErrorResponse } from '@angular/common/http';

import { GENERIC_REGISTRATION_ERROR, SERVER_UNREACHABLE_ERROR, registrationErrorMessages } from './registration-errors';

const httpError = (status: number, error: unknown) => new HttpErrorResponse({ status, error });

describe('registrationErrorMessages', () => {
  it('shows each Identity error description (shape returned by FilmScanner.Web)', () => {
    const error = httpError(400, {
      message: 'Failed to register user',
      errors: [
        { code: 'DuplicateUserName', description: "Username 'jane' is already taken." },
        { code: 'DuplicateEmail', description: "Email 'jane@example.com' is already taken." },
      ],
    });

    expect(registrationErrorMessages(error)).toEqual([
      "Username 'jane' is already taken.",
      "Email 'jane@example.com' is already taken.",
    ]);
  });

  it('shows ASP.NET validation problem details messages', () => {
    const error = httpError(400, {
      title: 'One or more validation errors occurred.',
      errors: { Email: ['The Email field is required.'], UserName: ['The UserName field is required.'] },
    });

    expect(registrationErrorMessages(error)).toEqual([
      'The Email field is required.',
      'The UserName field is required.',
    ]);
  });

  it('falls back to the message when there are no specific errors', () => {
    expect(registrationErrorMessages(httpError(400, { message: 'Invalid registration data' })))
      .toEqual(['Invalid registration data']);
    expect(registrationErrorMessages(httpError(400, { message: 'Failed to register user', errors: [] })))
      .toEqual(['Failed to register user']);
  });

  it('removes duplicate messages', () => {
    const error = httpError(400, { errors: [{ description: 'Same.' }, { description: 'Same.' }] });

    expect(registrationErrorMessages(error)).toEqual(['Same.']);
  });

  it('says the server could not be reached on a network error', () => {
    expect(registrationErrorMessages(httpError(0, new ProgressEvent('error')))).toEqual([SERVER_UNREACHABLE_ERROR]);
  });

  it('shows the generic message for unknown shapes', () => {
    expect(registrationErrorMessages(httpError(500, 'Internal Server Error'))).toEqual([GENERIC_REGISTRATION_ERROR]);
    expect(registrationErrorMessages(httpError(400, { errors: [{ code: 'NoDescription' }] }))).toEqual([GENERIC_REGISTRATION_ERROR]);
    expect(registrationErrorMessages(new Error('not an HTTP error'))).toEqual([GENERIC_REGISTRATION_ERROR]);
  });
});
