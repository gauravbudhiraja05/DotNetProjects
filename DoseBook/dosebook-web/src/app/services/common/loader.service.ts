import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { LoaderState } from '../../models/loader.model';


@Injectable({
  providedIn: 'root'
})
export class LoaderService {

  loaderSubject$ = new Subject<boolean>();

  loading = false;

  constructor() {
    this.loaderSubject$.subscribe((state) => {
      this.loading = state;
    });
  }

  show() {
    this.loaderSubject$.next(true);
  }

  hide() {
    this.loaderSubject$.next(false);
  }
}
