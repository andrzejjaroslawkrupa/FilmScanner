import { Component, Input, Output, OnChanges, SimpleChanges, HostListener } from '@angular/core';
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
export class CarouselComponent implements OnChanges {

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
  private _slidesPerPage = Math.round(this.getScreenWidth() / 400);

  constructor(private _router: Router) {
    this._debouncer
      .pipe(debounceTime(400))
      .subscribe((value) => this.isNextPageLast.emit(value));
  }

  @HostListener('window:resize')
  protected getScreenWidth(): number {
    return window.innerWidth;
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
