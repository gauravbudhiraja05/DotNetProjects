import { Component, ElementRef, Input, OnInit, ViewChild } from '@angular/core';
import { Test, MedicineDose, Advice } from 'src/app/models/medicine-dose.model';
import { AlertController, ModalController, NavController } from '@ionic/angular';
import { AddMedicinePage } from './add-medicine/add-medicine.page';
import * as _ from 'lodash';
import { Prescription } from 'src/app/models/prescription.model';
import { ClassificationResult, ClassificationType } from 'src/app/models/document.model';
import { Record } from 'src/app/models/medical-record';
import { ShowMedicalHistoryPage } from './show-medical-history/show-medical-history.page';
import { LocalDataService } from 'src/app/services/common/local-data.service';
import { SuggestionPanelComponent } from './suggestion-panel/suggestion-panel.component';
import { PrescriptionService } from 'src/app/services/http/presription.service';
import { empty, Subject } from 'rxjs';
import { catchError, debounceTime, distinctUntilChanged, switchMap, take, tap } from 'rxjs/operators';
import { LoaderService } from 'src/app/services/common/loader.service';
import { NotificationService } from 'src/app/services/common/notification.service';
import * as Sentry from '@sentry/browser';
import { PrescriptionMeta } from 'src/app/models/http/prescription-meta.response';
import { DictionaryDataService } from 'src/app/services/http/dictionary-data.service';

@Component({
  selector: 'app-new-edit-prescription',
  templateUrl: './new-edit-prescription.component.html',
  styleUrls: ['./new-edit-prescription.component.scss'],
})
export class NewEditPrescriptionComponent implements OnInit {

  selectedMedicineDose: MedicineDose;

  showEditDose = false;

  knownHistory: Record[];

  allergicTo: Record[];

  INBETWEEN_CHARS_REGEX = '[\\s\\.\\?!,]';

  prx: Prescription = {
    medicines: [],
    tests: [],
    suggestsions: []
  };

  @ViewChild(SuggestionPanelComponent)
  suggestionPanel: SuggestionPanelComponent;

  @ViewChild('problemInput')
  problemInput: ElementRef;

  problemTextChange: Subject<string> = new Subject<string>();

  searchResults: string[];
  searching = false;

  metaData: PrescriptionMeta[] = [];

  constructor(private modalCtr: ModalController,
              private alertCtrl: AlertController,
              private localDataService: LocalDataService,
              public loader: LoaderService,
              private dictionaryService: DictionaryDataService,
              private prxService: PrescriptionService,
              private notification: NotificationService) {
  }

  get problemText() {
    return this.prx.problem;
  }

  @Input()
  set problemText(value: string) {
    this.prx.problem = value;
    this.refreshClassifications();
  }

  inputChanged() {
    this.problemTextChange.next(this.problemText);
  }

  ngOnInit() {
    this.problemTextChange.pipe(
      tap(_ => this.searching = true),
      debounceTime(300),
      distinctUntilChanged(),
      switchMap((term: string) => term ? this.dictionaryService.loadDictionary(term) : empty()),
      tap(_ => this.searching = false)
    ).pipe(
      catchError((error, caught) => {
        this.searching = false;
        return caught;
      }))
      .subscribe(result => this.searchResults = result);
  }



  ionViewDidEnter() {
    this.suggestionPanel.setPrx = this.prx;
    this.localDataService.patient.pipe(take(1)).subscribe((patient) => {
      this.prx.patient = patient;
      this.initPrescriptionData();
    });
  }

  initPrescriptionData() {
    this.localDataService.prx.pipe(take(1)).subscribe((prx) => {
      if (prx) {
        this.prx.id = prx.id;
        this.prx.medicines = prx.medicines || [];
        this.prx.tests = prx.tests || [];
        this.prx.suggestsions = prx.suggestsions || [];
        this.problemText = prx.problem;
      }
    });
  }

  applyPrediction(prediction: string) {
    this.problemInput.nativeElement.focus();
    this.problemText = prediction;
    this.searchResults = [];
  }

  async showMedicalHistory() {
    const modal = await this.modalCtr.create({
      component: ShowMedicalHistoryPage,
      componentProps: {
        records: this.prx.patient.history
      }
    });
    await modal.present();
  }

  addToPrescription(result: ClassificationResult) {
    if (result.classification.type === ClassificationType.MEDICINE) {
      this.prx.medicines.push({ ...result.classification.dose });
    } else if (result.classification.type === ClassificationType.TEST) {
      this.prx.tests.push({ ...result.classification.test });
    } else if (result.classification.type === ClassificationType.ADVICE) {
      this.prx.suggestsions.push({ ...result.classification.advice });
    }
    this.refreshClassifications();
  }

  removeFromPrescription(medicine: MedicineDose) {
    // medicine to remove from suggested medicine list
    this.prx.medicines.splice(this.prx.medicines.indexOf(medicine), 1);
    this.refreshClassifications();
  }

  removeTestFromPrescription(test: Test) {
    this.prx.tests.splice(this.prx.tests.indexOf(test), 1);
    this.refreshClassifications();
  }

  removeAdviceFromPrescription(advice: Advice) {
    this.prx.suggestsions.splice(this.prx.suggestsions.indexOf(advice), 1);
    this.refreshClassifications();
  }

  isValidPrescrription(): boolean {
    return this.prx.medicines.length !== 0 && this.problemText?.length > 3;
  }

  editDose(medicine: MedicineDose) {
    this.showEditDose = true;
    this.selectedMedicineDose = medicine;
  }

  closeEditDoseModal() {
    this.showEditDose = false;
  }

  addTest() {
    this.prx.tests.push({
      testName: ''
    });
  }

  addAdvice() {
    this.prx.suggestsions.push({
      description: ''
    });
  }

  testSelected(selectedTest: Test, test: Test) {
    test.testName = selectedTest.testName;
    test.testId = selectedTest.testId;
  }

  adviceSelected(selectedAdvice: Advice, advice: Advice) {
    advice.id = selectedAdvice.id;
    advice.description = selectedAdvice.description;
  }

  private refreshClassifications(): void {
    this.suggestionPanel.setPrx = this.prx;
  }

  async prescribe() {
    try {
      if (this.prx?.id) {
        const alert = await this.alertCtrl.create({
          header: 'Prescription already saved?',
          message: 'Do you want to overwrite the existing prescriptions?',
          buttons: [{
            text: 'No, Print Only',
            handler: () => {
              console.log('print only');
            }
          }, {
            text: 'Yes',
            handler: async () => {
              await this.savePrescription();
            }
          }]
        });
        await alert.present();
        return;
      } else {
        await this.savePrescription();
      }

    } catch (error) {
      this.notification.error('Error saving prescription');
      this.loader.hide();
      throw error; // will be handled by sentry
    }
  }

  async addMedicine() {
    this.prx.medicines.push({
      composition: '',
      directions: '',
      dose: 0,
      doseUnit: '',
      duration: '',
      frequency: '',
      medicineName: ''
    });
  }

  async savePrescription() {
    this.loader.show();

    // Step 1 == Save prescription
    this.prx.id = await this.prxService.savePrescription(this.prx);

    this.notification.success('Prescription Saved Successfully.');
    this.loader.hide();
  }

  async addMedicinePopup() {
    const modal = await this.modalCtr.create({
      component: AddMedicinePage,
      cssClass: 'add-medicine-modal',
      backdropDismiss: false
    });

    modal.onDidDismiss().then((data) => {
      if (data.data) {
        this.addToPrescription({
          value: 1,
          classification: {
            type: ClassificationType.MEDICINE,
            dose: data.data as MedicineDose
          }
        });
      }
    });

    return await modal.present();
  }

}
