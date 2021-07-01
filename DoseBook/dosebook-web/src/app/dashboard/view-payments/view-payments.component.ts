import { Component, OnInit } from '@angular/core';
import { PatientPurchase } from 'src/app/models/patient-purchase.model';

@Component({
  selector: 'app-view-payments',
  templateUrl: './view-payments.component.html',
  styleUrls: ['./view-payments.component.scss'],
})
export class ViewPaymentsComponent implements OnInit {

  transactions: PatientPurchase[] = [];

  constructor() { }

  ngOnInit() {}

}
