import { Component, OnInit } from '@angular/core';
import { Clinic } from 'src/app/models/clinic.model';

@Component({
  selector: 'app-select-clinic',
  templateUrl: './select-clinic.component.html',
  styleUrls: ['./select-clinic.component.scss'],
})
export class SelectClinicComponent implements OnInit {

  clinics: Clinic[] = [
    {
      id: 1,
      name: 'Clinic1',
      place: 'Place1'
    },
    {
      id: 2,
      name: 'Clinic2',
      place: 'Place3'
    },
    {
      id: 3,
      name: 'Clinic3',
      place: 'Place3'
    },
    {
      id: 4,
      name: 'Clinic4',
      place: 'Place4'
    },
    {
      id: 5,
      name: 'Clinic5',
      place: 'Place5'
    }
  ];

  selectedClinicId: number = null;

  constructor() { }

  ngOnInit() {
    console.log(this.clinics);
  }

  selectClinic(clinicId: number) {
    this.selectedClinicId = clinicId;
  }

}
