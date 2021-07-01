import { NgModule } from '@angular/core';
import { IonicModule } from '@ionic/angular';
import { AgePipe } from './pipes/age.pipe';
import { PredictionFilterPipe } from './pipes/prediction-filter.pipe';

@NgModule({
  imports: [ IonicModule ],
  declarations: [
    AgePipe,
    PredictionFilterPipe
  ],
  exports: [
    AgePipe,
    PredictionFilterPipe
  ]
})
export class SharedModule {

}
