import { ComponentFixture, TestBed } from '@angular/core/testing';
import { provideRouter } from '@angular/router';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';

import { CarouselComponent } from './carousel.component';
import { Slide } from './carousel.interface';

const SLIDES_PER_PAGE = 3;

function createSlides(count: number): Slide[] {
  return Array.from({ length: count }, (_, i) => ({
    title: `title ${i}`,
    imageSrc: `image-${i}.jpg`,
    destination: '/film',
    parameterKey: 'id',
    parameterValue: `${i}`,
  }));
}

describe('CarouselComponent', () => {
  let component: CarouselComponent;
  let fixture: ComponentFixture<CarouselComponent>;
  let element: HTMLElement;

  const leftArrow = (): HTMLButtonElement =>
    element.querySelector('img[src$="arrow_left.svg"]')!.closest('button')!;
  const rightArrow = (): HTMLButtonElement =>
    element.querySelector('img[src$="arrow_right.svg"]')!.closest('button')!;
  const isVisible = (button: HTMLButtonElement): boolean => !button.hidden;
  const displayedTitles = (): string[] =>
    Array.from(element.querySelectorAll('.mini-box p')).map(p => p.textContent!.trim());

  async function setSlides(slides: Slide[]): Promise<void> {
    fixture.componentRef.setInput('slides', slides);
    fixture.detectChanges();
    await fixture.whenStable();
    fixture.detectChanges();
  }

  async function click(button: HTMLButtonElement): Promise<void> {
    button.click();
    fixture.detectChanges();
    await fixture.whenStable();
    fixture.detectChanges();
  }

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [NoopAnimationsModule, CarouselComponent],
      providers: [provideRouter([])],
    }).compileComponents();

    fixture = TestBed.createComponent(CarouselComponent);
    component = fixture.componentInstance;
    element = fixture.nativeElement;
    // Page size is derived from window width; pin it so tests don't depend on the browser size.
    (component as any)._slidesPerPage = SLIDES_PER_PAGE;
  });

  it('should create', () => {
    fixture.detectChanges();
    expect(component).toBeTruthy();
  });

  describe('right arrow', () => {
    it('is hidden when there are no slides', async () => {
      await setSlides([]);

      expect(isVisible(rightArrow())).toBe(false);
    });

    it('is hidden when all slides fit on a single page', async () => {
      await setSlides(createSlides(SLIDES_PER_PAGE));

      expect(isVisible(rightArrow())).toBe(false);
      expect(isVisible(leftArrow())).toBe(false);
    });

    it('is hidden on the first page even when further pages exist', async () => {
      await setSlides(createSlides(7));

      expect(isVisible(rightArrow())).toBe(false);
      expect(isVisible(leftArrow())).toBe(true);
    });

    it('is shown after moving away from the first page', async () => {
      await setSlides(createSlides(7));

      await click(leftArrow());

      expect(component.currentPage).toBe(-1);
      expect(isVisible(rightArrow())).toBe(true);
    });

    it('is shown on the last page, while the left arrow is hidden', async () => {
      await setSlides(createSlides(7));

      await click(leftArrow());
      await click(leftArrow());

      expect(displayedTitles()).toEqual(['title 6']);
      expect(isVisible(rightArrow())).toBe(true);
      expect(isVisible(leftArrow())).toBe(false);
    });

    it('navigates to a page that has slides to display', async () => {
      await setSlides(createSlides(7));
      await click(leftArrow());

      await click(rightArrow());

      expect(displayedTitles()).toEqual(['title 2', 'title 1', 'title 0']);
    });

    it('is hidden again after navigating back to the first page', async () => {
      await setSlides(createSlides(7));
      await click(leftArrow());

      await click(rightArrow());

      expect(component.currentPage).toBe(0);
      expect(isVisible(rightArrow())).toBe(false);
      expect(isVisible(leftArrow())).toBe(true);
    });

    it('is hidden after slides are replaced, since the carousel resets to the first page', async () => {
      await setSlides(createSlides(7));
      await click(leftArrow());

      await setSlides(createSlides(5));

      expect(component.currentPage).toBe(0);
      expect(isVisible(rightArrow())).toBe(false);
    });
  });

  describe('loading more slides', () => {
    it('keeps the current page when more slides are appended', async () => {
      const firstBatch = createSlides(7);
      await setSlides(firstBatch);
      await click(leftArrow());

      await setSlides([...firstBatch, ...createSlides(10).slice(7)]);

      expect(component.currentPage).toBe(-1);
      expect(displayedTitles()).toEqual(['title 5', 'title 4', 'title 3']);
      expect(isVisible(rightArrow())).toBe(true);
    });

    it('starts over when slides are replaced with a different list of the same size', async () => {
      await setSlides(createSlides(7));
      await click(leftArrow());

      await setSlides(createSlides(7));

      expect(component.currentPage).toBe(0);
      expect(displayedTitles()).toEqual(['title 2', 'title 1', 'title 0']);
    });
  });
});
