import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Title } from '@angular/platform-browser';

@Injectable()
export class AppTitleService {
  public constructor(private titleService: Title) { }
  private title = new BehaviorSubject<string>('Home');
  private title$ = this.title.asObservable();

  setTitle(title: string) {
    this.title.next(title);
    this.titleService.setTitle('FilmScanner - ' + title);
  }

  getTitle(): Observable<string> {
    return this.title$;
  }
}
