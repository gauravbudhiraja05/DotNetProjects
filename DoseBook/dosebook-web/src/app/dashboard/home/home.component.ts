import { Component, OnInit } from '@angular/core';
import { ModalController } from '@ionic/angular';
import { AddPatientPage } from './add-patient/add-patient.page';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent implements OnInit {

  constructor(public modalController: ModalController) { }

  ngOnInit() {}

}
