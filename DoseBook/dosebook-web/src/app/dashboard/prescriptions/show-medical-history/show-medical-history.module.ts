import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { ShowMedicalHistoryPageRoutingModule } from './show-medical-history-routing.module';

import { ShowMedicalHistoryPage } from './show-medical-history.page';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    ShowMedicalHistoryPageRoutingModule
  ],
  declarations: [ShowMedicalHistoryPage]
})
export class ShowMedicalHistoryPageModule {}
