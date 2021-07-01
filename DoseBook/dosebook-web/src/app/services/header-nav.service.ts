import { Injectable } from '@angular/core';
import { BehaviorSubject, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class HeaderNavService {

  selectedMenu = new Subject();

  constructor() { }

  menuItemClicked(itemName: string) {
    this.selectedMenu.next(itemName);
  }

}
