import { Component } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  isPremieresChosen: boolean = true;
  isMostPopularChosen: boolean = false;
  isComediesChosen: boolean = false;
  isThrillersChosen: boolean = false;
  isActionChosen: boolean = false;

  resetAllBooleans(){
    this.isPremieresChosen = false;
    this.isMostPopularChosen = false;
    this.isComediesChosen = false;
    this.isThrillersChosen = false;
    this.isActionChosen = false;
  }
}
