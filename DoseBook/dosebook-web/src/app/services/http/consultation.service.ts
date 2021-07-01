import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { computeStackId } from '@ionic/angular/directives/navigation/stack-utils';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Constants } from 'src/app/helpers/constants';
import { Consultation } from 'src/app/models/consultation.model';
import { ServiceResponse } from 'src/app/models/service-response';

@Injectable({
  providedIn: 'root'
})
export class ConsultationService {

  constructor(private http: HttpClient) { }

  getConsultations(): Observable<Consultation[]> {
    return this.http.get<Consultation[]>(`${Constants.GET_CONSULTATIONS}`);
  }

  createConsultation(consultation: Consultation): Promise<number> {
    const { email, name, dob, mobile, gender } = consultation.patient;
    return this.http.post<CreateConsultationResponse>(`${Constants.CREATE_CONSULTATION}`,
      { email, name, dob, mobile, gender }).pipe(map((data) => {
        return data.id;
      })).toPromise();
  }

  createConsultationByPatientId(patientId: number): Promise<number> {
    return this.http.post<CreateConsultationResponse>(`${Constants.CREATE_CONSULTATION}`,
      { patient_id: patientId }).pipe(map((data) => {
        return data.id;
      })).toPromise();
  }

  removeConsultation(consultation: Consultation): Promise<void> {
    return this.http.delete<void>(`${Constants.BASE_URL}/doctor/consultation/${consultation.id}`).toPromise();
  }

}

class CreateConsultationResponse {
  'doctor_id': number;
  'patient_id': number;
  'status': string;
  'created_at': string;
  'updated_at': string;
  'id': number;
}
