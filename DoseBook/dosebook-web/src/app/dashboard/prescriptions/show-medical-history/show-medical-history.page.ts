import { Component, Input, OnInit } from '@angular/core';
import { ModalController } from '@ionic/angular';
import { Record, RecordType } from 'src/app/models/medical-record';
import { PatientService } from 'src/app/services/http/patient.service';

@Component({
  selector: 'app-show-medical-history',
  templateUrl: './show-medical-history.page.html',
  styleUrls: ['./show-medical-history.page.scss'],
})
export class ShowMedicalHistoryPage implements OnInit {

  @Input()
  records: Record[];

  knownHistory: Record[] = [];

  allergicTo: Record[] = [];

  addHistoryForm = false;
  addAllergicForm = false;

  historyRecordValue = '';
  allergicRecordValue = '';

  constructor(private modalCtrl: ModalController, private patientService: PatientService) { }

  ngOnInit() {

    this.knownHistory = this.records.filter((record) => {
      return record.type === RecordType.HISTORY;
    });

    this.allergicTo = this.records.filter((record) => {
      return record.type === RecordType.DIAGNOSTIC;
    });

  }

  async closePopup() {
    await this.modalCtrl.dismiss();
  }

  async addHistory() {
    await this.addRecord(RecordType.HISTORY);
  }

  async addAllergy() {
    await this.addRecord(RecordType.DIAGNOSTIC);
  }

  async addRecord(type: RecordType) {
    try {
      const savedRecord = await this.patientService.saveRecord(this.historyRecordValue, 1, type);
      this.knownHistory.push(savedRecord);
    } catch (error) {
      console.log(error);
    }
    this.showHistoryForm(false);
    this.showAllergicRecordForm(false);
  }

  showHistoryForm(show: boolean) {
    this.historyRecordValue = '';
    this.addHistoryForm = show;
  }

  showAllergicRecordForm(show: boolean) {
    this.allergicRecordValue = '';
    this.addAllergicForm = show;
  }

}
