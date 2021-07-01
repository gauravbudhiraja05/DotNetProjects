import { Component, OnInit } from '@angular/core';
import { ModalController } from '@ionic/angular';
import { ClassificationType, Document } from 'src/app/models/document.model';
import { MedicineDose } from 'src/app/models/medicine-dose.model';
import { EditDoseComponent } from 'src/app/dashboard/prescriptions/edit-dose/edit-dose.component';

@Component({
  selector: 'app-medicine-setting',
  templateUrl: './medicine-setting.page.html',
  styleUrls: ['./medicine-setting.page.scss'],
})
export class MedicineSettingPage implements OnInit {

  documents: Document[] = [];

  constructor(private modalCtrl: ModalController) {
    this.documents.push({
      label: 'Crocin 500',
      description: [
        'fever',
        'age_40_50'
      ],
      result: {
        value: 0,
        classification: {
          type: ClassificationType.MEDICINE,
          dose: {
            directions: 'After meal',
            dose: 1,
            doseUnit: 'tablet',
            duration: '2 Weeks',
            frequency: 'X1/week',
            composition: 'Crocin 500',
            id: 1,
            medicineName: 'Crocin 500'
          },

        }
      }
    }, {
      label: 'Flexson 500',
      description: [
        'pain',
        'age_40_50'
      ],
      result: {
        value: 0,
        classification: {
          type: ClassificationType.MEDICINE,
          dose: {
            directions: 'After meal',
            dose: 1,
            doseUnit: 'tablet',
            duration: '2 Weeks',
            frequency: 'X1/week',
            composition: 'Flexson 500',
            id: 1,
            medicineName: 'Flexson 500'
          }
        }
      }
    });
  }

  ngOnInit() {
  }

  async openEditDoseModal(medicineDose: MedicineDose) {
    const modal = await this.modalCtrl.create({
      component: EditDoseComponent,
      componentProps: {
        medicineDose
      }
    });
    await modal.present();
  }

}
