import { Component, OnInit, Input, AfterViewInit } from '@angular/core';
import { Slide } from './carousel.inteface';

@Component({
  selector: 'app-carousel',
  templateUrl: './carousel.component.html',
  styleUrls: ['./carousel.component.css']
})
export class CarouselComponent  {
@Input() slides: Slide[] = [];

slidesPerPage = 5;
currentPage = 0;

  constructor() { }


}
