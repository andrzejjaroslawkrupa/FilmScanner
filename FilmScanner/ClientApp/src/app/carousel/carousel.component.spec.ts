import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { CarouselComponent } from './carousel.component';
import { Router } from '@angular/router';
import { RouterTestingModule } from '@angular/router/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

describe('CarouselComponent', () => {
  let component: CarouselComponent;
  let fixture: ComponentFixture<CarouselComponent>;
  let element: HTMLElement;

  beforeEach(waitForAsync(() => {
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

  it('should not show arrows on first and last page', () => {
    const arrowElement = element.querySelectorAll('button');

    component.slides = [{
      title: 'title', imageSrc: '', destination: '', parameterKey: '', parameterValue: ''
    }];
    component.currentPage = 0;
    fixture.detectChanges();

    expect(arrowElement[0].hasAttribute('hidden')).toEqual(true);
    expect(arrowElement[1].hasAttribute('hidden')).toEqual(true);
  });

});
