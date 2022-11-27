import { Component, Input, IterableDiffers, DoCheck, Output, OnChanges, SimpleChanges, HostListener } from '@angular/core';
import { Slide } from './carousel.interface';
import { Router } from '@angular/router';
import { EventEmitter } from '@angular/core';
import { Subject } from 'rxjs';
import { debounceTime } from 'rxjs/operators';
import { trigger, state, style, transition, animate, keyframes } from '@angular/animations';

@Component({
  selector: 'app-carousel',
  templateUrl: './carousel.component.html',
  styleUrls: ['./carousel.component.scss'],
  animations: [
    trigger('transform', [
      state('visible-center', style({
        opacity: 1,
        marginLeft: 0,
        marginRight: 0
      })),
      state('vanished-left', style({
        opacity: 0,
        marginLeft: '20vw',
        marginRight: '-20vw'
      })),
      state('vanished-right', style({
        opacity: 0,
        marginLeft: '-20vw',
        marginRight: '20vw'
      })),
      transition('vanished-left => visible-center', [
        animate('0.6s', keyframes([
          style({ opacity: 0, marginLeft: '-20vw', marginRight: '20vw' }),
          style({ opacity: 1, marginLeft: 0, marginRight: 0 })
        ]))
      ]),
      transition('vanished-right => visible-center', [
        animate('0.6s', keyframes([
          style({ opacity: 0, marginLeft: '20vw', marginRight: '-20vw' }),
          style({ opacity: 1, marginLeft: 0, marginRight: 0 })
        ]))
      ]),
      transition('* <=> void', [
        animate('0s')
      ]),
      transition('* <=> *', [
        animate('0.6s')
      ]),
    ]),
  ],

})
export class CarouselComponent implements DoCheck, OnChanges {

  @Input() slides: Slide[];
  @Input() currentPage = 0;
  @Input() defaultImageSrc: string;
  @Output() isNextPageLast: EventEmitter<boolean> = new EventEmitter<boolean>();

  private _debouncer: Subject<boolean> = new Subject<boolean>();

  animationState = 'visible-center';
  slidesLoaded = false;
  isCurrentPageLast = false;
  slidesLoader = true;
  imageLoader = true;

  bufferedSlides: Slide[] = [];
  private _slidesPerPage = Math.round(this.getScreenWidth() / 400);
  private _iterableDiffer: any;

  constructor(iterableDiffers: IterableDiffers, private _router: Router) {
    this._iterableDiffer = iterableDiffers.find([]).create(null);
    this._debouncer
      .pipe(debounceTime(400))
      .subscribe((value) => this.isNextPageLast.emit(value));
  }

  @HostListener('window:resize', ['$event'])
  private getScreenWidth(event?): number {
    return window.innerWidth;
  }

  ngOnChanges(_changes: SimpleChanges): void {
    this.slidesLoaded = false;
    this.currentPage = 0;
    this.populateBuffer();
  }

  ngDoCheck(): void {
    const changes = this._iterableDiffer.diff(this.slides);
    if (changes) {
      this.populateBuffer();
    }
  }

  previousPage(): void {
    this.animationState = 'vanished-left';
    this.currentPage--;
  }

  nextPage(): void {
    this.animationState = 'vanished-right';
    this.currentPage++;
  }

  populateBuffer(): void {
    this.bufferedSlides = this.slides.slice(this.startingIndex(), this.endingIndex()).reverse();
    if (this.getIsPreviousPageLast() && this.slides.length > 0) {
      this._debouncer.next(this.getIsPreviousPageLast());
    }

    this.updateIsCurrentPageLast();
    this.resetAnimationsState();
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

  private resetAnimationsState() {
    if (this.animationState === 'vanished-right' || this.animationState === 'vanished-left') {
      this.animationState = 'visible-center';
    }
  }

  goToRouterLink(destination: string, parameterKey: string, parameterValue: string): void {
    const url = `${destination}?${parameterKey}=${parameterValue}`;
    this._router.navigateByUrl(url);
  }

}
