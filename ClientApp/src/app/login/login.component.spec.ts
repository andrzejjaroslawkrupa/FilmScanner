import { ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpErrorResponse } from '@angular/common/http';
import { provideRouter } from '@angular/router';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { Subject } from 'rxjs';

import { LoginComponent } from './login.component';
import { AuthService } from '../services/auth.service';

describe('LoginComponent', () => {
  let fixture: ComponentFixture<LoginComponent>;
  let element: HTMLElement;
  let loginResponse: Subject<{ token: string }>;
  let loginCalls: [string, string][];

  beforeEach(async () => {
    loginCalls = [];
    loginResponse = new Subject();
    const authService = {
      login: (username: string, password: string) => {
        loginCalls.push([username, password]);
        return loginResponse.asObservable();
      },
      saveAuthResponseToLocalStorage: () => {},
    };

    await TestBed.configureTestingModule({
      imports: [NoopAnimationsModule, LoginComponent],
      providers: [provideRouter([]), { provide: AuthService, useValue: authService }],
    }).compileComponents();

    fixture = TestBed.createComponent(LoginComponent);
    element = fixture.nativeElement;
    fixture.detectChanges();
  });

  function fill(name: string, value: string): void {
    const input = element.querySelector<HTMLInputElement>(`input[name="${name}"]`)!;
    input.value = value;
    input.dispatchEvent(new Event('input'));
  }

  /** Clicks submit and reports whether the browser's native form submission was cancelled. */
  function clickSubmit(): { defaultPrevented: boolean | undefined } {
    const result: { defaultPrevented: boolean | undefined } = { defaultPrevented: undefined };
    const record = (e: Event) => (result.defaultPrevented = e.defaultPrevented);
    document.addEventListener('submit', record);
    element.querySelector<HTMLButtonElement>('button[type="submit"]')!.click();
    document.removeEventListener('submit', record);
    return result;
  }

  it('submits the entered credentials', () => {
    fill('usename', 'jane');
    fill('password', 'Secret1!');

    clickSubmit();

    expect(loginCalls).toEqual([['jane', 'Secret1!']]);
  });

  it('prevents the native form submission that would put credentials in the URL', () => {
    fill('usename', 'jane');
    fill('password', 'Secret1!');

    const submit = clickSubmit();

    expect(submit.defaultPrevented).toBe(true);
  });

  it('shows an error message when login fails', async () => {
    vi.spyOn(console, 'error').mockImplementation(() => {}); // the component logs the error
    fill('usename', 'jane');
    fill('password', 'Wrong1!!');
    clickSubmit();

    loginResponse.error(new HttpErrorResponse({ status: 400, error: { message: 'Invalid credentials' } }));
    await fixture.whenStable();
    fixture.detectChanges();

    expect(element.textContent).toContain('There was an error during login attempt');
  });
});
