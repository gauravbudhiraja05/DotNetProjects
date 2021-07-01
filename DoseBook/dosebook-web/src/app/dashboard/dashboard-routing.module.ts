import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { DashboardPage } from './dashboard.page';
import { HomeComponent } from './home/home.component';
import { ManageTeamComponent } from './manage-team/manage-team.component';
import { NewEditPrescriptionComponent } from './prescriptions/new-edit-prescription.component';
import { ViewAllPrescriptionsComponent } from './prescriptions/view-all-prescriptions/view-all-prescriptions.component';
import { GeneralSettingPage } from './settings/general-setting/general-setting.page';
import { MedicineSettingPage } from './settings/medicine-setting/medicine-setting.page';
import { PaymentSettingPage } from './settings/payment-setting/payment-setting.page';
import { PrescriptionSettingPage } from './settings/prescription-setting/prescription-setting.page';
import { SettingsComponent } from './settings/settings.component';
import { ViewPatientComponent } from './view-patient/view-patient.component';
import { ViewPaymentsComponent } from './view-payments/view-payments.component';

const routes: Routes = [
  {
    path: '',
    component: DashboardPage,
    children: [
      { path: '', component: HomeComponent },
      { path: 'view-payments', component: ViewPaymentsComponent },
      { path: 'manage-team', component: ManageTeamComponent },
      { path: 'settings', component: SettingsComponent },
      {
        path: 'add-patient',
        loadChildren: () => import('./home/add-patient/add-patient.module').then(m => m.AddPatientPageModule)
      }, {
        path: 'add-patient',
        loadChildren: () => import('./home/add-patient/add-patient.module').then(m => m.AddPatientPageModule)
      },
      {
        path: 'general-setting',
        component: GeneralSettingPage
      },
      {
        path: 'medicine-setting',
        component: MedicineSettingPage
      },
      {
        path: 'prescription-setting',
        component: PrescriptionSettingPage
      },
      {
        path: 'payment-setting',
        component: PaymentSettingPage
      },
      {
        path: 'new-edit-prescription',
        component: NewEditPrescriptionComponent
      },
      {
        path: 'add-medicine',
        loadChildren: () => import('./prescriptions/add-medicine/add-medicine.module').then(m => m.AddMedicinePageModule)
      },
      {
        path: 'view-prescription',
        loadChildren: () => import('./prescriptions/view-all-prescriptions/view-prescription/view-prescription.module').then(m => m.ViewPrescriptionPageModule)
      },
      {
        path: 'view-all-prescriptions',
        component: ViewAllPrescriptionsComponent
      },
      {
        path: 'view-patient/:id',
        component: ViewPatientComponent
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class DashboardPageRoutingModule { }
