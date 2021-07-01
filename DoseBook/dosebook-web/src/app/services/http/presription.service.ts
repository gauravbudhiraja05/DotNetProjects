import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Constants } from 'src/app/helpers/constants';
import { Observable } from 'rxjs';
import { Prescription } from 'src/app/models/prescription.model';
import { map } from 'rxjs/operators';
import { PrescriptionResponse } from 'src/app/models/http/prescription.response';
import { PrxMedicineResponse } from 'src/app/models/http/prx-medicine.response';
import { Test, MedicineDose, Advice } from 'src/app/models/medicine-dose.model';
import { PrxTestResponse } from 'src/app/models/http/prx-test.response';
import { PrxAdviceResponse } from 'src/app/models/http/prx-advice.response';
import { CreateOrUpdatePrxRequest } from 'src/app/models/http/create-update-prescription.request';
import { CreatePrxMedicineRequest } from 'src/app/models/http/create-prx-medicine.request';

@Injectable({
  providedIn: 'root'
})
export class PrescriptionService {

  constructor(private http: HttpClient) { }

  // service methods
  savePrescription(prx: Prescription): Promise<number> {
    return this.http.post<number>(`${Constants.BASE_URL}/doctor/${prx.patient.id}/prescription`, this.toCreateOrUpdatePrxRequest(prx))
      .toPromise();
  }

  loadPrescriptionByPatientId(id: number): Observable<Prescription[]> {
    return this.http.get<PrescriptionResponse[]>(`${Constants.BASE_URL}/doctor/${id}/prescriptions`)
      .pipe(map((data) => this.toPrescriptions(data)));
  }

  private toCreateOrUpdatePrxRequest(prescription: Prescription): CreateOrUpdatePrxRequest {
    return {
      prescription_id: prescription.id,
      medicines: this.fromMedicineDoses(prescription.medicines),
      problem: prescription.problem,
      next_visit_due: prescription.nextVisitDue ? prescription.nextVisitDue.toLocaleDateString() : null,
      tests: [],
      suggestions: []
    };
  }

  private toPrescription(prxResponse: PrescriptionResponse): Prescription {
    return {
      id: prxResponse.id,
      doctorId: prxResponse.doctor_id,
      problem: prxResponse.problem,
      medicines: this.toMedicineDoses(prxResponse.medicines),
      tests: this.formatTest(prxResponse.tests),
      suggestsions: this.formatAdvice(prxResponse.advices),
      nextVisitDue: new Date(prxResponse.next_visit_due),
      created: new Date(prxResponse.created_at),
      updated: new Date(prxResponse.updated_at)
    };
  }

  // Formatter methods
  private toPrescriptions(prescriptionList: PrescriptionResponse[]): Prescription[] {
    const prescriptions: Prescription[] = [];
    prescriptionList.forEach((prxResponse: PrescriptionResponse) => {
      prescriptions.push(this.toPrescription(prxResponse));
    });
    return prescriptions;
  }

  private toMedicineDoses(prxMedicines: PrxMedicineResponse[]): MedicineDose[] {
    const medicines: MedicineDose[] = [];
    prxMedicines.forEach(prxMedicine => {
      medicines.push({
        id: prxMedicine.id,
        medicineName: prxMedicine.medicine_name,
        frequency: prxMedicine.frequency,
        directions: prxMedicine.directions,
        composition: prxMedicine.composition,
        duration: prxMedicine.duration,
        dose: Number(prxMedicine.dose),
        doseUnit: prxMedicine.dose_unit
      });
    });
    return medicines;
  }

  private formatTest(prxTests: PrxTestResponse[]): Test[] {
    const tests: Test[] = [];
    prxTests.forEach(test => {
      tests.push({
        testId: test.id,
        testName: test.test_name
      });
    });
    return tests;
  }

  private formatAdvice(prxAdvices: PrxAdviceResponse[]): Advice[] {
    const advices: Advice[] = [];
    prxAdvices.forEach(advice => {
      advices.push({
        id: advice.id,
        description: advice.description
      });
    });
    return advices;
  }

  private fromMedicineDoses(medicineDoses: MedicineDose[]): CreatePrxMedicineRequest[] {
    const prxMedicines: CreatePrxMedicineRequest[] = [];
    medicineDoses.forEach((dose) => {
      prxMedicines.push({
        medicine_name: dose.medicineName,
        frequency: dose.frequency,
        composition: dose.composition,
        directions: dose.directions,
        duration: dose.duration,
        dose: String(dose.dose),
        dose_unit: dose.doseUnit
      });
    });
    return prxMedicines;
  }

}
