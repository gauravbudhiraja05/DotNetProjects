import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.scss'],
})
export class SettingsComponent implements OnInit {

  step = 1;

  constructor() { }

  ngOnInit() {}

  changeStep(step: number) {
    this.step = step;
  }

}
