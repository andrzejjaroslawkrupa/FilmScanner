import { Component, OnInit, signal } from '@angular/core';
import { Profile } from './profile';
import { ProfileService } from '../services/profile.service';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [],
  templateUrl: './profile.component.html'
})
export class ProfileComponent implements OnInit {
  constructor(private _profileService: ProfileService) { }
  public userName = signal("");
  ngOnInit(): void {
    this._setUserName();
  }
  private _setUserName(): void {
    this._profileService.getProfile().subscribe((p: Profile) => this.userName.set(p.userName));
  }
}