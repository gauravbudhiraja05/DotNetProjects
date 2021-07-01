import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Constants } from 'src/app/helpers/constants';
import { BehaviorSubject, Observable } from 'rxjs';
import { ServiceResponse } from 'src/app/models/service-response';
import { Record, RecordType } from 'src/app/models/medical-record';
import { Patient } from 'src/app/models/patient.model';

@Injectable({
  providedIn: 'root'
})
export class PatientService {

  constructor(private http: HttpClient) { }

  saveRecord(record: string, patientId: number, type: RecordType): Promise<Record> {
    return this.http.post<Record>(Constants.SAVE_PATIENT_HISTORY + patientId, {
      record,
      type
    }).toPromise();
  }

  searchByNumber(mobile: string): Promise<Patient[]> {
    return this.http.get<Patient[]>(Constants.SEARCH_PATIENT + `?mobile=${mobile}`).toPromise();
  }

  savePatient(patient: Patient): Promise<Patient> {
    return this.http.post<Patient>(Constants.ADD_PATIENT, patient).toPromise();
  }

}
