import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-reset-pin',
  templateUrl: './reset-pin.page.html',
  styleUrls: ['./reset-pin.page.scss'],
})
export class ResetPinPage implements OnInit {


  inputValue = '';

  constructor() { }

  ngOnInit() {
  }

  numberClicked(key: number | string) {
    if (key === 'D') {
      this.inputValue = this.inputValue.slice(0, this.inputValue.length - 1);
    } else if (this.inputValue.length < 4) {
      this.inputValue += key;
    }

  }

}
