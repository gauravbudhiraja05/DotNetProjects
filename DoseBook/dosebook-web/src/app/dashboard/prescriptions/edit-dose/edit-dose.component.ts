import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { PrescriptionMeta } from 'src/app/models/http/prescription-meta.response';
import { MedicineDose } from 'src/app/models/medicine-dose.model';
import { LocalDataService } from 'src/app/services/common/local-data.service';
import { MedicineService } from 'src/app/services/http/medicine.service';
import * as _ from 'lodash';

@Component({
  selector: 'app-edit-dose',
  templateUrl: './edit-dose.component.html',
  styleUrls: ['./edit-dose.component.scss'],
})
export class EditDoseComponent implements OnInit {

  @Input()
  medicineDose: MedicineDose;

  @Input()
  medtaData: PrescriptionMeta[];

  allDoses: number[] = [];

  allFrequencies: string[] = [];

  allDirections: string[] = [];

  allDurations: string[] = [];

  doseUnits: string[] = [];

  constructor(private localDataService: LocalDataService, private medicineService: MedicineService) {
    this.allFrequencies = this.localDataService.frequencies;
    this.allDoses = this.localDataService.doses;
    this.allDirections = this.localDataService.directions;
    this.allDurations = this.localDataService.durations;
    this.doseUnits = this.localDataService.doseUnits;
   }

  ngOnInit() {
  }

  async medicineSelected(response: MedicineDose) {
    // this.medicineDose = _.clone(response);
    this.medicineDose.frequency = response.frequency;
    this.medicineDose.directions = response.directions;
    this.medicineDose.dose = response.dose;
    this.medicineDose.doseUnit = response.doseUnit;
    this.medicineDose.duration = response.duration;
    this.medicineDose.composition = response.composition;
    this.medicineDose.medicineName = response.medicineName;
    this.medicineDose.id = response.id;
  }

}
