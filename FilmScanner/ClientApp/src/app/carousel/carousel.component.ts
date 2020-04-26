import { Component, OnInit, Input, AfterViewInit, IterableDiffers, DoCheck } from '@angular/core';
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

  ngDoCheck() {
    const changes = this._iterableDiffer.diff(this.slides);
    if (changes) {
      this.bufferedSlides = this.slides.slice(0, this.slidesPerPage).reverse();
    }
  }

  selectFilm(imdbID: string) {
    this._router.navigate(['/film-overview'],
      { queryParams: { filmID: imdbID } });
  }

}
