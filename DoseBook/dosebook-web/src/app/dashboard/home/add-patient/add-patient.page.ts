import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ModalController } from '@ionic/angular';
import { Patient } from 'src/app/models/patient.model';
import { NotificationService } from 'src/app/services/common/notification.service';
import { PatientService } from 'src/app/services/http/patient.service';
import * as _ from 'lodash';
import { ConsultationService } from 'src/app/services/http/consultation.service';
import { LoaderComponent } from 'src/app/loader/loader.component';
import { LoaderService } from 'src/app/services/common/loader.service';
import { Consultation } from 'src/app/models/consultation.model';
import { ConsultationStatus } from 'src/app/models/enum/consultation-status.enum';

@Component({
  selector: 'app-add-patient',
  templateUrl: './add-patient.page.html',
  styleUrls: ['./add-patient.page.scss'],
})
export class AddPatientPage implements OnInit {

  addPatientForm: FormGroup;

  searchResults: Patient[] = [];

  @Input()
  searchText: string;

  constructor( private formBuilder: FormBuilder,
               private modalCtrl: ModalController,
               private loader: LoaderService,
               private consultationService: ConsultationService,
               private notificationService: NotificationService ) {
    this.addPatientForm = this.formBuilder.group({
      name: ['', [Validators.required, Validators.minLength(4) ]],
      mobile: ['', [Validators.required, Validators.minLength(10), Validators.maxLength(10)]],
      dob: [new Date(), Validators.required],
      email: ['', Validators.email],
      gender: ['', Validators.required]
    });
   }

  ngOnInit() {
    console.log(this.searchText);
    this.addPatientForm.patchValue({
      mobile: this.searchText
    });
  }

  dismiss() {
    this.modalCtrl.dismiss();
  }

  async savePatient() {
    if (this.addPatientForm.valid) {
      const patient = this.addPatientForm.value as Patient;
      try {
        this.searchText = '';
        this.searchResults = [];
        this.loader.show();
        const consultation: Consultation = {
          status: ConsultationStatus.WAITING,
          patient
        };
        consultation.id = await this.consultationService.createConsultation(consultation);
        this.loader.hide();
        this.modalCtrl.dismiss(consultation);
      } catch (error) {
        this.loader.hide();
      }
    }else{
      this.notificationService.error('Invalid Details');
    }
  }

}
