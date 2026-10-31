import { ComponentFixture, TestBed, fakeAsync, tick } from '@angular/core/testing';
import { provideRouter } from '@angular/router';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { Subject } from 'rxjs';

import { NavMenuComponent } from './nav-menu.component';
import { FilmsService } from '../services/films.service';
import { AuthService } from '../services/auth.service';
import { SearchResult } from '../films/searchResult';

const DEBOUNCE = NavMenuComponent.SEARCH_DEBOUNCE_MS;

function result(title: string): SearchResult {
  return {
    searches: [{ title, year: '1999', imdbID: `id-${title}`, type: 'movie', poster: 'N/A' }],
    totalResults: '1',
    response: 'True',
  };
}

describe('NavMenuComponent search', () => {
  let component: NavMenuComponent;
  let fixture: ComponentFixture<NavMenuComponent>;
  // One controllable response stream per request, keyed by search term.
  let requests: { term: string; response: Subject<SearchResult> }[];

  beforeEach(async () => {
    requests = [];
    const filmsService = {
      searchFilmsByTitle: (term: string) => {
        const response = new Subject<SearchResult>();
        requests.push({ term, response });
        return response.asObservable();
      },
    };

    await TestBed.configureTestingModule({
      imports: [NoopAnimationsModule, NavMenuComponent],
      providers: [
        provideRouter([]),
        { provide: FilmsService, useValue: filmsService },
        { provide: AuthService, useValue: { isAuthenticated: () => false, logout: () => {} } },
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(NavMenuComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  const respond = (term: string, data: SearchResult) => {
    requests.find(r => r.term === term)!.response.next(data);
  };
  const shownTitle = () => component.films()?.searches[0].title;

  it('sends a single request for the final term when typing quickly', fakeAsync(() => {
    component.onInput('m');
    component.onInput('ma');
    component.onInput('matrix');
    tick(DEBOUNCE);

    expect(requests.map(r => r.term)).toEqual(['matrix']);
  }));

  it('ignores a late response for an older term (out-of-order responses)', fakeAsync(() => {
    component.onInput('ma');
    tick(DEBOUNCE);
    component.onInput('matrix');
    tick(DEBOUNCE);
    expect(requests.map(r => r.term)).toEqual(['ma', 'matrix']);

    respond('matrix', result('The Matrix'));
    respond('ma', result('Stale result for "ma"'));

    expect(shownTitle()).toBe('The Matrix');
  }));

  it('trims the term and does not repeat a request for the same term', fakeAsync(() => {
    component.onInput('matrix ');
    tick(DEBOUNCE);
    component.onInput(' matrix');
    tick(DEBOUNCE);

    expect(requests.map(r => r.term)).toEqual(['matrix']);
  }));

  it('clears results without a request when the input is emptied', fakeAsync(() => {
    component.onInput('matrix');
    tick(DEBOUNCE);
    respond('matrix', result('The Matrix'));
    expect(shownTitle()).toBe('The Matrix');

    component.onInput('   ');
    tick(DEBOUNCE);

    expect(component.films()).toBeUndefined();
    expect(requests.length).toBe(1);
  }));

  it('keeps searching after a failed request', fakeAsync(() => {
    component.onInput('broken');
    tick(DEBOUNCE);
    requests[0].response.error(new Error('network'));
    expect(component.films()).toBeUndefined();

    component.onInput('matrix');
    tick(DEBOUNCE);
    respond('matrix', result('The Matrix'));

    expect(shownTitle()).toBe('The Matrix');
  }));
});
