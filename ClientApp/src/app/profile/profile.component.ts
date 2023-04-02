import { Component, OnInit } from '@angular/core';
import { Profile } from '../login/profile';
import { ProfileService } from '../services/profile.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html'
})
export class ProfileComponent implements OnInit {
  constructor(private _profileService: ProfileService) { }

  public userName: string = "";

  ngOnInit(): void {
    this._setUserName();
  }

  private _setUserName(): void {
    this._profileService.getProfile().subscribe((p: Profile): string => this.userName = p.userName);
  }
}
