import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Constants } from 'src/app/helpers/constants';
import { GlobalData } from 'src/app/models/global-data';
import { Patient } from 'src/app/models/patient.model';
import { Storage } from '@ionic/storage';
import { Prescription } from 'src/app/models/prescription.model';
import { MedicineService } from '../http/medicine.service';

@Injectable({
  providedIn: 'root'
})
export class LocalDataService {

  private selectedPatient$: BehaviorSubject<Patient> = new BehaviorSubject<Patient>(null);

  private selectedPrx$: BehaviorSubject<Prescription> = new BehaviorSubject<Prescription>(null);

  frequencies: string[] = [];
  doseUnits: string[] = [];
  directions: string[] = [];
  durations: string[] = [];
  doses: number[] = [];

  constructor(private storage: Storage, private medicineService: MedicineService) {
    this.storage.get(Constants.SELECTED_PATIENT).then((patient) => {
      this.setPatient = patient;
    });

    this.storage.get(Constants.SELECTED_PRX).then((prx) => {
      this.setPrx = prx;
    });

    this.medicineService.loadMeta().subscribe((data) => {
      data.forEach((item) => {

        switch (item.type) {
          case 'FREQUENCY':
            this.frequencies.push(item.value as string);
            break;

          case 'DOSEUNIT':
            this.doseUnits.push(item.value as string);
            break;

          case 'DOSE':
            this.doses.push(item.value as number);
            break;

          case 'DURATION':
            this.durations.push(item.value as string);
            break;

          case 'DIRECTION':
            this.directions.push(item.value as string);
            break;

          default:
            break;
        }
      });
    });
  }

  get prx(): Observable<Prescription> {
    return this.selectedPrx$.asObservable();
  }

  get patient(): Observable<Patient> {
    return this.selectedPatient$.asObservable();
  }

  set setPatient(patient: Patient) {
    this.storage.set(Constants.SELECTED_PATIENT, patient);
    this.selectedPatient$.next(patient);
  }

  set setPrx(prx: Prescription) {
    this.storage.set(Constants.SELECTED_PRX, prx);
    this.selectedPrx$.next(prx);
  }

}
