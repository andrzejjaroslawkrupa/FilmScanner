import { AfterViewInit, ChangeDetectorRef, Component, DestroyRef, ElementRef, Input, OnChanges, Output, SimpleChanges, ViewChild } from '@angular/core';
import { Slide } from './carousel.interface';
import { Router } from '@angular/router';
import { EventEmitter } from '@angular/core';
import { Subject } from 'rxjs';
import { debounceTime } from 'rxjs/operators';

import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

@Component({
  selector: 'app-carousel',
  standalone: true,
  imports: [MatProgressSpinnerModule],
  templateUrl: './carousel.component.html',
  styleUrls: ['./carousel.component.scss'],
})
export class CarouselComponent implements OnChanges, AfterViewInit {

  @Input() slides: Slide[] = [];
  @Input() currentPage = 0;
  @Input() defaultImageSrc: string ='';
  @Output() isNextPageLast: EventEmitter<boolean> = new EventEmitter<boolean>();

  private _debouncer: Subject<boolean> = new Subject<boolean>();

  slidesLoaded = false;
  isCurrentPageLast = false;
  slidesLoader = true;
  imageLoader = true;

  bufferedSlides: Slide[] = [];
  /** Direction of the last page change; drives the page enter/leave CSS animations. */
  private _direction: 'left' | 'right' | null = null;
  /** Width a slide needs: the 180px poster (see .image) plus the column gutter (2 × 12px). */
  static readonly SLIDE_MIN_WIDTH = 204;
  // Rough estimate until the slides area has been measured (see ngAfterViewInit).
  private _slidesPerPage = Math.max(1, Math.round(window.innerWidth / 400));

  @ViewChild('slidesArea', { static: true }) private _slidesArea!: ElementRef<HTMLElement>;

  constructor(private _router: Router, private _cdr: ChangeDetectorRef, private _destroyRef: DestroyRef) {
    this._debouncer
      .pipe(debounceTime(400))
      .subscribe((value) => this.isNextPageLast.emit(value));
  }

  ngAfterViewInit(): void {
    // Fit as many slides as the slides area can hold, and refit whenever it is resized.
    if (typeof ResizeObserver === 'undefined') {
      return;
    }
    const area = this._slidesArea.nativeElement;
    const observer = new ResizeObserver(() => this.updateSlidesPerPage(area.getBoundingClientRect().width));
    observer.observe(area);
    this._destroyRef.onDestroy(() => observer.disconnect());
  }

  /** Recomputes how many slides fit in the given width, keeping the first visible slide on screen. */
  updateSlidesPerPage(width: number): void {
    const perPage = Math.max(1, Math.floor(width / CarouselComponent.SLIDE_MIN_WIDTH));
    if (perPage === this._slidesPerPage) {
      return;
    }
    const firstVisible = this.startingIndex();
    this._slidesPerPage = perPage;
    this.currentPage = -Math.floor(firstVisible / perPage);
    this._direction = null; // re-laying out, not paging: no animation
    this.populateBuffer();
    this._cdr.markForCheck(); // ResizeObserver callbacks don't trigger change detection on their own
  }

  ngOnChanges(changes: SimpleChanges): void {
    const slidesChange = changes['slides'];
    // Appending more slides (infinite loading) keeps the current page; replacing them starts over.
    if (!slidesChange || !this.isAppend(slidesChange.previousValue, slidesChange.currentValue)) {
      this.slidesLoaded = false;
      this.currentPage = 0;
      this._direction = null; // starting over: show the first page without animating
    }
    this.populateBuffer();
  }

  private isAppend(previous: Slide[] | undefined, current: Slide[]): boolean {
    return !!previous && previous.length > 0 && current.length > previous.length
      && previous.every((slide, i) => slide === current[i]);
  }

  previousPage(): void {
    this._direction = 'left';
    this.currentPage--;
    this.populateBuffer();
  }

  nextPage(): void {
    this._direction = 'right';
    this.currentPage++;
    this.populateBuffer();
  }

  // The new page comes in from the side of the arrow that was clicked and the old one leaves the other way.
  get enterAnimation(): string {
    return this._direction ? `page-enter-from-${this._direction}` : '';
  }

  get leaveAnimation(): string {
    return this._direction ? `page-leave-to-${this._direction === 'left' ? 'right' : 'left'}` : '';
  }

  populateBuffer(): void {
    this.bufferedSlides = this.slides.slice(this.startingIndex(), this.endingIndex()).reverse();
    if (this.getIsPreviousPageLast() && this.slides.length > 0) {
      this._debouncer.next(this.getIsPreviousPageLast());
    }

    this.updateIsCurrentPageLast();
  }

  private startingIndex(): number {
    return -1 * this.currentPage * this._slidesPerPage;
  }

  private endingIndex(): number {
    return this.startingIndex() + this._slidesPerPage;
  }

  private getIsPreviousPageLast(): boolean {
    return (this.endingIndex() + this._slidesPerPage) >= this.slides.length;
  }

  private updateIsCurrentPageLast(): void {
    this.isCurrentPageLast = this.endingIndex() >= this.slides.length;
  }

  goToRouterLink(destination: string, parameterKey: string, parameterValue: string): void {
    const url = `${destination}?${parameterKey}=${parameterValue}`;
    this._router.navigateByUrl(url);
  }

}
