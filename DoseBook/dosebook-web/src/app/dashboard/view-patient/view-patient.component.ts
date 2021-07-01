import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ModalController, NavController } from '@ionic/angular';
import { Subscription } from 'rxjs';
import { take, takeUntil } from 'rxjs/operators';
import { LoaderService } from 'src/app/services/common/loader.service';
import { environment } from 'src/environments/environment';
import { GlobalData } from '../../models/global-data';
import { Patient } from '../../models/patient.model';
import { Prescription } from '../../models/prescription.model';
import { LocalDataService } from '../../services/common/local-data.service';
import { PrescriptionService } from '../../services/http/presription.service';

@Component({
  selector: 'app-view-patient',
  templateUrl: './view-patient.component.html',
  styleUrls: ['./view-patient.component.scss'],
})
export class ViewPatientComponent implements OnInit {

  prescriptions: Prescription[] = [];

  patient: Patient;

  selectedIndex = -1;
  selectedPrescription: Prescription = null;

  constructor(private navController: NavController,
              private prxService: PrescriptionService,
              private dataService: LocalDataService,
              public loaderService: LoaderService,
              private route: ActivatedRoute) {
  }

  ngOnInit() {

  }

  ionViewWillEnter() {
    this.loaderService.show();
    this.dataService.patient.pipe(take(1)).subscribe((patient: Patient) => {
      this.patient = patient;
      if (this.patient) {
        this.prxService.loadPrescriptionByPatientId(this.patient.id).pipe(take(1)).subscribe((prx) => {
          this.prescriptions = prx;
          if (this.prescriptions.length > 0) {
            this.selectedIndex = this.prescriptions.length - 1;
            this.selectedPrescription = this.prescriptions[this.selectedIndex];
          }
          this.loaderService.hide();
        }, err => {
          this.loaderService.hide();
          throw err;
        });
      }
    });
  }

  navigateNext() {
    this.selectedIndex++;
    this.selectedPrescription = this.prescriptions[this.selectedIndex];
  }

  navigatePrevious() {
    this.selectedIndex--;
    this.selectedPrescription = this.prescriptions[this.selectedIndex];
  }

  backToHome() {
    this.navController.navigateRoot('dashboard');
  }


  async showCreatePrxForm() {
    this.dataService.setPrx = null;
    this.navController.navigateRoot('dashboard/new-edit-prescription');
  }

  async copyAndCreate(prescription: Prescription) {
    const newPrx = { ... prescription };
    newPrx.id = undefined;
    this.dataService.setPrx = newPrx;
    this.navController.navigateRoot('dashboard/new-edit-prescription');
  }

  async editPrescription(prescription: Prescription) {
    this.dataService.setPrx = prescription;
    this.navController.navigateRoot('dashboard/new-edit-prescription');
  }

}
