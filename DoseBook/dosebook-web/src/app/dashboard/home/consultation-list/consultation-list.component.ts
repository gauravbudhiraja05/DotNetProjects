import { Component, Input, OnInit } from '@angular/core';
import { ModalController, NavController } from '@ionic/angular';
import { Patient } from 'src/app/models/patient.model';
import { ConsultationStatus } from 'src/app/models/enum/consultation-status.enum';
import { Consultation } from 'src/app/models/consultation.model';
import { LocalDataService } from 'src/app/services/common/local-data.service';
import { AddPatientPage } from '../add-patient/add-patient.page';
import * as _ from 'lodash';
import { ConsultationService } from 'src/app/services/http/consultation.service';
import { LoaderService } from 'src/app/services/common/loader.service';
import { PatientService } from 'src/app/services/http/patient.service';
@Component({
  selector: 'app-consultation-list',
  templateUrl: './consultation-list.component.html',
  styleUrls: ['./consultation-list.component.scss'],
})
export class ConsultationListComponent implements OnInit {

  selectedStatus = 'waiting';

  searchResults: Patient[] = [];
  allConsultations: Consultation[] = [];

  filteredConsultations: Consultation[] = [];

  searching = false;

  searchText = '';

  constructor(private navContoller: NavController,
              private dataService: LocalDataService,
              private modalCtrl: ModalController,
              private loader: LoaderService,
              private patientService: PatientService,
              private consultationService: ConsultationService) {
  }

  ngOnInit() {
    this.loader.show();
    this.consultationService.getConsultations().subscribe((consultations) => {
      this.loader.hide();
      this.allConsultations = consultations;
      this.showPatients(this.selectedStatus);
    }, error => {
      this.loader.hide();
    });
  }

  viewPatient(patient: Patient) {
    this.dataService.setPatient = patient;
    this.navContoller.navigateRoot(`dashboard/view-patient/${patient.id}`);
  }

  showPatients(status: string) {
    this.searchText = '';
    this.searchResults = [];
    this.selectedStatus = status;
    if (status === 'all') {
      this.filteredConsultations = this.allConsultations;
    } else {
      this.filteredConsultations = this.allConsultations.filter((st) => st.status === status);
    }
  }

  async addNewConsultation(patient: Patient) {
    try {
      this.searchText = '';
      this.searchResults = [];
      this.loader.show();

      const consultationId = await this.consultationService.createConsultationByPatientId(patient.id);
      const consultation: Consultation = {
        id: consultationId,
        patient,
        status: ConsultationStatus.WAITING
      };
      this.allConsultations.push(consultation);

      this.showPatients(this.selectedStatus);
      this.loader.hide();
    } catch (error) {
      this.loader.hide();
    }

  }

  async removeConsultation(consultation: Consultation) {
    try {
      this.loader.show();
      await this.consultationService.removeConsultation(consultation);
      this.allConsultations.splice(this.allConsultations.indexOf(consultation), 1);
      this.showPatients(this.selectedStatus);
      this.loader.hide();
    } catch (error) {
      this.loader.hide();
    }
  }

  async search() {
    if (this.searchText.length === 10) {
      this.searching = true;
      try {
        this.searchResults = await this.patientService.searchByNumber(this.searchText);
        this.searching = false;
      } catch (error) {
        console.log(error);
        this.searching = false;
      }
    } else {
      this.searching = false;
      this.searchResults = [];
    }
  }

  async showAddPatient() {
    const modal = await this.modalCtrl.create({
      component: AddPatientPage,
      cssClass: 'my-custom-class',
      backdropDismiss: false,
      componentProps: {
        searchText: this.searchText
      }
    });

    modal.onDidDismiss().then(async (data) => {
      if (data.data) {
        const consultation = data.data as Consultation;
        this.allConsultations.push(consultation);
        this.showPatients(this.selectedStatus);
      }
    });

    return await modal.present();
  }

clear(){
  this.searchText = ''
}
}
