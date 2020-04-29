import { Component, Input, IterableDiffers, DoCheck, Output, OnChanges, SimpleChanges, HostListener } from '@angular/core';
import { Slide } from './carousel.inteface';
import { Router } from '@angular/router';
import { EventEmitter } from '@angular/core';
import { Subject } from 'rxjs';
import { debounceTime } from 'rxjs/operators';

@Component({
  selector: 'app-carousel',
  templateUrl: './carousel.component.html',
  styleUrls: ['./carousel.component.css']
})
export class CarouselComponent implements DoCheck, OnChanges {

  @Input() slides: Slide[];
  @Input() currentPage = 0;
  @Output() isNextPageLast: EventEmitter<boolean> = new EventEmitter<boolean>();

  debouncer: Subject<boolean> = new Subject<boolean>();

  bufferedSlides: Slide[] = [];
  private _slidesPerPage = Math.round(this.getScreenWidth() / 400);
  private _iterableDiffer: any;

  constructor(iterableDiffers: IterableDiffers, private _router: Router) {
    this._iterableDiffer = iterableDiffers.find([]).create(null);
    this.debouncer
      .pipe(debounceTime(400))
      .subscribe((value) => this.isNextPageLast.emit(value));
  }

  @HostListener('window:resize', ['$event'])
  private getScreenWidth(event?): number {
    return window.innerWidth;
  }

  ngOnChanges(_changes: SimpleChanges): void {
    this.currentPage = 0;
    this.populateBuffer();
  }

  ngDoCheck(): void {
    const changes = this._iterableDiffer.diff(this.slides);
    if (changes) {
      this.populateBuffer();
    }
  }

  private getIsNextPageLast(): boolean {
    return (this.endingIndex() + this._slidesPerPage) >= this.slides.length;
  }

  previousPage(): void {
    this.currentPage--;
    this.populateBuffer();
  }

  nextPage(): void {
    this.currentPage++;
    this.populateBuffer();
  }

  private startingIndex(): number {
    return -1 * this.currentPage * this._slidesPerPage;
  }

  private endingIndex(): number {
    return this.startingIndex() + this._slidesPerPage;
  }

  private populateBuffer(): void {
    this.bufferedSlides = this.slides.slice(this.startingIndex(), this.endingIndex()).reverse();
    if (this.getIsNextPageLast() && this.slides !== []) {
      this.debouncer.next(this.getIsNextPageLast());
    }
  }

  goToRouterLink(destination: string, parameterKey: string, parameterValue: string): void {
    const url = `${destination}?${parameterKey}=${parameterValue}`;
    this._router.navigateByUrl(url);
  }

}
