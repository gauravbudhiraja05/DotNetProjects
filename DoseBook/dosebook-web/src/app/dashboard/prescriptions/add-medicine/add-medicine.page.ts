import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { ModalController } from '@ionic/angular';
import { ClassificationResult } from 'src/app/models/document.model';
import { MedicineDose } from 'src/app/models/medicine-dose.model';

@Component({
  selector: 'app-add-medicine',
  templateUrl: './add-medicine.page.html',
  styleUrls: ['./add-medicine.page.scss'],
})
export class AddMedicinePage implements OnInit {

  medicines: MedicineDose[] = [];

  @Output()
  selectedMedicine: EventEmitter<MedicineDose> = new EventEmitter();

  constructor(private modalCtrl: ModalController) { }

  ngOnInit() {
  }

  async search($event) {
    const value = $event.detail.value;
    if (value.length === 0) {
      this.medicines = [];
    } else {
      this.medicines =  [{
        directions: 'After meal',
        dose: 1,
        doseUnit: 'tablet',
        duration: '2 Weeks',
        frequency: 'X 1/Day',
        composition: 'Paracetamol',
        id: 1,
        medicineName: 'Crocin 500'
      }];
    }
  }


  async selectMedicine(result: ClassificationResult) {
    this.modalCtrl.dismiss(result);
  }

  async dismiss() {
    this.modalCtrl.dismiss();
  }

}
