import { Component, OnInit } from '@angular/core';
import { ModalController } from '@ionic/angular';
import { contains } from 'src/app/helpers/util';
import { Prescription } from 'src/app/models/prescription.model';
import { ViewPrescriptionPage } from './view-prescription/view-prescription.page';

@Component({
  selector: 'app-view-all-prescriptions',
  templateUrl: './view-all-prescriptions.component.html',
  styleUrls: ['./view-all-prescriptions.component.scss'],
})
export class ViewAllPrescriptionsComponent implements OnInit {

  prescriptions: Prescription[] = [];

  allPrescriptions = [];

  constructor(private modalCtr: ModalController) {
    this.allPrescriptions.push({
      id: 1,
      created: new Date(),
      doctorId: 1,
      patient: {
        age: 24,
        gender: 'Male',
        name: 'Test',
        mobile: '999999',
        dob: new Date()
      },
      problem: 'Pain, Fever',
      status: 'PRESCRRIBED',
      updated: new Date(),
      medicines: [{
        directions: 'After Meal',
        dose: 1,
        doseUnit: 'Tablet',
        duration: '2 Weeks',
        frequency: 'X day',
        label: 'Crocin 500',
        medicineId: 1,
        medicineName: 'crocin 500'
      }]
    });
   }

  ngOnInit() {
    this.prescriptions = this.allPrescriptions;
  }

  filterPrx($event) {
    const searchText = $event.detail.value;
    if (searchText.length > 0) {
      this.prescriptions = this.allPrescriptions.filter((prx) => {
        return contains(prx.patient.name, searchText) || contains(prx.patient.mobile, searchText);
      });
    } else {
      this.prescriptions = this.allPrescriptions;
    }
  }

  async viewPrescription(prx: Prescription) {
    const modal = await this.modalCtr.create({
      component: ViewPrescriptionPage,
      cssClass: 'view-prescription-modal',
      backdropDismiss: false,
      componentProps: {
        prescription: prx
      }
    });

    return await modal.present();
  }

}
