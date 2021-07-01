import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { DashboardPageRoutingModule } from './dashboard-routing.module';

import { DashboardPage } from './dashboard.page';
import { HeaderComponent } from './common/header/header.component';
import { HomeComponent } from './home/home.component';
import { SharedModule } from '../shared.module';
import { PrintService } from '../services/print.service';
import { ConsultationListComponent } from './home/consultation-list/consultation-list.component';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    DashboardPageRoutingModule,
    SharedModule
  ],
  declarations: [
    DashboardPage,
    HeaderComponent,
    HomeComponent,
    ConsultationListComponent
  ],
  providers: [
    PrintService
  ],
  entryComponents: [ ],
  exports: [HeaderComponent]
})
export class DashboardPageModule { }
