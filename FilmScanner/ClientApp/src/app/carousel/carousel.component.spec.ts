import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CarouselComponent } from './carousel.component';
import { Router } from '@angular/router';
import { RouterTestingModule } from '@angular/router/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

describe('CarouselComponent', () => {
  let component: CarouselComponent;
  let fixture: ComponentFixture<CarouselComponent>;
  let element: HTMLElement;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [BrowserAnimationsModule],
      declarations: [CarouselComponent],
      providers: [
        {
          provide: Router,
          useClass: RouterTestingModule
        }
      ]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CarouselComponent);
    component = fixture.componentInstance;
    element = fixture.nativeElement;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should not show right-arrow on page 1', () => {
    const rightArrowElement = element.querySelectorAll('button');

    component.currentPage = 0;
    fixture.detectChanges();

    expect(rightArrowElement[0].hasAttribute('hidden')).toEqual(false);
    expect(rightArrowElement[1].hasAttribute('hidden')).toEqual(true);
  });

});
