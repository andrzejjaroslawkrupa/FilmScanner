import { BehaviorSubject } from 'rxjs';
import { IHttpState } from './iHttpState';
import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root'
  })
  export class HttpStateService {
    public state = new BehaviorSubject<IHttpState>({} as IHttpState);

    constructor() { }
  }
