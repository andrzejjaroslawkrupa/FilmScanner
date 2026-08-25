import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { CarouselComponent } from './carousel.component';
import { Router } from '@angular/router';
import { RouterTestingModule } from '@angular/router/testing';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';

describe('CarouselComponent', () => {
  let component: CarouselComponent;
  let fixture: ComponentFixture<CarouselComponent>;
  let element: HTMLElement;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      imports: [NoopAnimationsModule, CarouselComponent],
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
  });

  it('should create', () => {
    fixture.detectChanges();
    expect(component).toBeTruthy();
  });

  it('should not show arrows on first and last page', () => {
    component.slides = [{
      title: 'title', imageSrc: '', destination: '', parameterKey: '', parameterValue: ''
    }];
    component.currentPage = 0;
    component.populateBuffer();
    fixture.detectChanges();

    const arrowElement = element.querySelectorAll('button');

    expect(arrowElement[0].hasAttribute('hidden')).toEqual(true);
    expect(arrowElement[1].hasAttribute('hidden')).toEqual(true);
  });

});