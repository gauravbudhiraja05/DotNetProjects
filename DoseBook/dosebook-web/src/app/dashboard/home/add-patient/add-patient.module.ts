import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormsModule, ReactiveFormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { AddPatientPageRoutingModule } from './add-patient-routing.module';

import { AddPatientPage } from './add-patient.page';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    ReactiveFormsModule,
    AddPatientPageRoutingModule
  ],
  declarations: [AddPatientPage],
  providers: [FormBuilder]
})
export class AddPatientPageModule { }
