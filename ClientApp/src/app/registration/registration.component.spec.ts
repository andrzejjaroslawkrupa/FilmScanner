import { ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpErrorResponse } from '@angular/common/http';
import { provideRouter } from '@angular/router';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { Subject } from 'rxjs';

import { RegistrationComponent } from './registration.component';
import { RegisterService } from '../services/register.service';
import { Register } from './register';

describe('RegistrationComponent', () => {
  let fixture: ComponentFixture<RegistrationComponent>;
  let element: HTMLElement;
  let registerResponse: Subject<unknown>;
  let registered: Register[];

  beforeEach(async () => {
    registered = [];
    registerResponse = new Subject();
    const registerService = {
      register: (user: Register) => {
        registered.push(user);
        return registerResponse.asObservable();
      },
    };

    await TestBed.configureTestingModule({
      imports: [NoopAnimationsModule, RegistrationComponent],
      providers: [provideRouter([]), { provide: RegisterService, useValue: registerService }],
    }).compileComponents();

    fixture = TestBed.createComponent(RegistrationComponent);
    element = fixture.nativeElement;
    fixture.detectChanges();
  });

  async function fillValidForm(): Promise<void> {
    const values: Record<string, string> = {
      username: 'jane', email: 'jane@example.com', password: 'Secret1!', repeatpassword: 'Secret1!',
    };
    for (const [name, value] of Object.entries(values)) {
      const input = element.querySelector<HTMLInputElement>(`input[name="${name}"]`)!;
      input.value = value;
      input.dispatchEvent(new Event('input'));
    }
    await fixture.whenStable();
    fixture.detectChanges();
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

  it('enables submit only once the form is valid', async () => {
    const button = element.querySelector<HTMLButtonElement>('button[type="submit"]')!;
    expect(button.disabled).toBe(true);

    await fillValidForm();

    expect(button.disabled).toBe(false);
  });

  it('submits the entered data and prevents the native form submission', async () => {
    await fillValidForm();

    const submit = clickSubmit();

    expect(registered).toEqual([{ userName: 'jane', email: 'jane@example.com', password: 'Secret1!' }]);
    expect(submit.defaultPrevented).toBe(true);
  });

  it("shows the server's reasons when registration fails", async () => {
    vi.spyOn(console, 'error').mockImplementation(() => {}); // the component logs the error
    await fillValidForm();
    clickSubmit();

    registerResponse.error(new HttpErrorResponse({
      status: 400,
      error: { errors: [{ code: 'DuplicateUserName', description: "Username 'jane' is already taken." }] },
    }));
    await fixture.whenStable();
    fixture.detectChanges();

    expect(element.querySelector('[role="alert"]')?.textContent).toContain("Username 'jane' is already taken.");
  });
});
