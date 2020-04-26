import { Component, Input, IterableDiffers, DoCheck } from '@angular/core';
import { Slide } from './carousel.inteface';
import { Router } from '@angular/router';

@Component({
  selector: 'app-carousel',
  templateUrl: './carousel.component.html',
  styleUrls: ['./carousel.component.css']
})
export class CarouselComponent implements DoCheck {

  @Input() slides: Slide[];

  bufferedSlides: Slide[] = [];
  slidesPerPage = 3;
  currentPage = 0;
  private _iterableDiffer: any;

  constructor(iterableDiffers: IterableDiffers, private _router: Router) {
    this._iterableDiffer = iterableDiffers.find([]).create(null);
  }

  previousPage() {
    this.currentPage--;
    this.populateBuffer();
  }

  nextPage() {
    this.currentPage++;
    this.populateBuffer();
  }

  private startingIndex() {
    return -1 * this.currentPage * this.slidesPerPage;
  }

  private endingIndex() {
    return this.startingIndex() + this.slidesPerPage;
  }

  ngDoCheck() {
    const changes = this._iterableDiffer.diff(this.slides);
    if (changes) {
      this.populateBuffer();
    }
  }

  private populateBuffer() {
    this.bufferedSlides = this.slides.slice(this.startingIndex(), this.endingIndex()).reverse();
  }

  goToRouterLink(destination: string, parameterKey: string, parameterValue: string) {
    const url = `${destination}?${parameterKey}=${parameterValue}`;
    this._router.navigateByUrl(url);
  }

}
