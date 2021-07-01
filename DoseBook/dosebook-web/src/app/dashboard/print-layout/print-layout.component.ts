import { Component, Input, OnInit } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Test, MedicineDose, Advice } from 'src/app/models/medicine-dose.model';
import { Patient } from 'src/app/models/patient.model';
import { Prescription } from 'src/app/models/prescription.model';

@Component({
  selector: 'app-print-layout',
  templateUrl: './print-layout.component.html',
  styleUrls: ['./print-layout.component.scss'],
})
export class PrintLayoutComponent implements OnInit {


  @Input()
  prx: Prescription;

  constructor() { }

  ngOnInit() {
  }

}
