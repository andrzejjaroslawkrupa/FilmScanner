import { HttpErrorResponse } from '@angular/common/http';

export const GENERIC_REGISTRATION_ERROR = 'There was an error during registration attempt. Please try again.';
export const SERVER_UNREACHABLE_ERROR = 'Could not reach the server. Please check your connection and try again.';

/**
 * Turns a failed registration response into messages to show the user.
 *
 * Handles the shapes FilmScanner.Web returns:
 * - ASP.NET Identity failures: `{ message, errors: [{ code, description }] }`
 * - ASP.NET validation problem details: `{ errors: { Field: ['message'] } }`
 * - a plain `{ message }`
 * Falls back to a generic message for anything else.
 */
export function registrationErrorMessages(error: unknown): string[] {
  if (!(error instanceof HttpErrorResponse)) {
    return [GENERIC_REGISTRATION_ERROR];
  }
  if (error.status === 0) {
    return [SERVER_UNREACHABLE_ERROR];
  }

  const body = error.error;
  const errors = body?.errors;
  let messages: string[] = [];

  if (Array.isArray(errors)) {
    messages = errors.map(e => e?.description).filter(isNonEmptyString);
  } else if (errors && typeof errors === 'object') {
    messages = Object.values(errors).flat().filter(isNonEmptyString);
  }
  if (messages.length === 0 && isNonEmptyString(body?.message)) {
    messages = [body.message];
  }

  return messages.length > 0 ? [...new Set(messages)] : [GENERIC_REGISTRATION_ERROR];
}

function isNonEmptyString(value: unknown): value is string {
  return typeof value === 'string' && value.trim().length > 0;
}
