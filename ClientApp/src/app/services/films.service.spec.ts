import { TestBed } from '@angular/core/testing';
import { provideHttpClient } from '@angular/common/http';
import { HttpTestingController, provideHttpClientTesting } from '@angular/common/http/testing';

import { FilmsService } from './films.service';
import { environment } from '../../environments/environment';

describe('FilmsService', () => {
  let service: FilmsService;
  let http: HttpTestingController;
  const base = environment.omdbApiAddress;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [FilmsService, provideHttpClient(), provideHttpClientTesting()],
    });
    service = TestBed.inject(FilmsService);
    http = TestBed.inject(HttpTestingController);
  });

  afterEach(() => http.verify());

  const cases: [string, string][] = [
    ['matrix', 'matrix'],
    ['fast & furious', 'fast%20%26%20furious'],
    ['AC/DC', 'AC%2FDC'],
    ['what?', 'what%3F'],
    ['#9', '%239'],
    ['100%', '100%25'],
    ['Amélie', 'Am%C3%A9lie'],
  ];

  cases.forEach(([title, encoded]) => {
    it(`encodes the search title "${title}" as a single path segment`, () => {
      service.searchFilmsByTitle(title, 2).subscribe();

      http.expectOne(`${base}/search/${encoded}/2`).flush({ searches: [], totalResults: '0', response: 'True' });
    });
  });

  it('defaults to page 1', () => {
    service.searchFilmsByTitle('matrix').subscribe();

    http.expectOne(`${base}/search/matrix/1`).flush({});
  });

  it('encodes the film ID', () => {
    service.searchFilmByID('tt0133093').subscribe();
    http.expectOne(`${base}/film/tt0133093`).flush({});

    service.searchFilmByID('a/b?c').subscribe();
    http.expectOne(`${base}/film/a%2Fb%3Fc`).flush({});
  });
});
