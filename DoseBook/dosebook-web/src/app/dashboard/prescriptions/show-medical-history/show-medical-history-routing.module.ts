import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ShowMedicalHistoryPage } from './show-medical-history.page';

const routes: Routes = [
  {
    path: '',
    component: ShowMedicalHistoryPage
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ShowMedicalHistoryPageRoutingModule {}
