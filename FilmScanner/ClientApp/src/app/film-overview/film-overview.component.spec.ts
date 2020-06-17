import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FilmOverviewComponent } from './film-overview.component';
import { AppTitleService } from '../services/appTitle.service';
import { ActivatedRoute } from '@angular/router';
import { of } from 'rxjs';
import { FilmsService } from '../films/films.service';

describe('FilmOverviewComponent', () => {
  let component: FilmOverviewComponent;
  let fixture: ComponentFixture<FilmOverviewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FilmOverviewComponent ],
      providers: [
        {provide: AppTitleService, useValue: {getTitle: () => 'title'}},
        {provide: ActivatedRoute, useValue: {params: of({id: 123})} },
        {provide: FilmsService, useValue: {searchFilmByID: of()}}
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FilmOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
   expect(component).toBeTruthy();
  });
});
