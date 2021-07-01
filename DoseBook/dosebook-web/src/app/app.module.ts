import { APP_INITIALIZER, ErrorHandler, NgModule } from '@angular/core';
import { Router } from '@angular/router';
import * as Sentry from '@sentry/angular';

import { BrowserModule } from '@angular/platform-browser';
import { RouteReuseStrategy } from '@angular/router';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { IonicModule, IonicRouteStrategy } from '@ionic/angular';
import { SplashScreen } from '@ionic-native/splash-screen/ngx';
import { StatusBar } from '@ionic-native/status-bar/ngx';

import { IonicStorageModule } from '@ionic/storage';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { ScreenOrientation } from '@ionic-native/screen-orientation/ngx';
import { AuthenticationService } from './services/authentication.service';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { LoaderComponent } from './loader/loader.component';
import { SelectClinicComponent } from './select-clinic/select-clinic.component';
import { NewEditPrescriptionComponent } from './dashboard/prescriptions/new-edit-prescription.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ViewPatientComponent } from './dashboard/view-patient/view-patient.component';
import { EditDoseComponent } from './dashboard/prescriptions/edit-dose/edit-dose.component';
import { PatientService } from './services/http/patient.service';
import { ViewAllPrescriptionsComponent } from './dashboard/prescriptions/view-all-prescriptions/view-all-prescriptions.component';
import { ViewPaymentsComponent } from './dashboard/view-payments/view-payments.component';
import { ManageTeamComponent } from './dashboard/manage-team/manage-team.component';
import { SettingsComponent } from './dashboard/settings/settings.component';
import { GeneralSettingPage } from './dashboard/settings/general-setting/general-setting.page';
import { MedicineSettingPage } from './dashboard/settings/medicine-setting/medicine-setting.page';
import { PrescriptionSettingPage } from './dashboard/settings/prescription-setting/prescription-setting.page';
import { PaymentSettingPage } from './dashboard/settings/payment-setting/payment-setting.page';
import { ResetPinPage } from './dashboard/settings/general-setting/reset-pin/reset-pin.page';
import { SuggestionPanelComponent } from './dashboard/prescriptions/suggestion-panel/suggestion-panel.component';
import { LocalDataService } from './services/common/local-data.service';
import { SharedModule } from './shared.module';
// import { Printer } from '@ionic-native/printer';
import { Printer } from '@ionic-native/printer/ngx';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login/login.component';
import { PrintService } from './services/print.service';
import { PrintLayoutComponent } from './dashboard/print-layout/print-layout.component';
import { ResponseInterceptor } from './interceptors/response.interceptor';
import { RequestInterceptor } from './interceptors/request.interceptor';
import { AuthGuard } from './services/auth-guard.service';
import { PredictionFilterPipe } from './pipes/prediction-filter.pipe';
import { environment } from 'src/environments/environment';
import { GenericSearchComponent } from './dashboard/prescriptions/generic-search/generic-search.component';

@NgModule({
  declarations: [
    AppComponent,
    LoaderComponent,
    LoginComponent,
    SelectClinicComponent,
    NewEditPrescriptionComponent,
    ViewPaymentsComponent,
    ViewPatientComponent,
    EditDoseComponent,
    ViewAllPrescriptionsComponent,
    SuggestionPanelComponent,
    ManageTeamComponent,
    SettingsComponent,
    GeneralSettingPage,
    MedicineSettingPage,
    PrescriptionSettingPage,
    PaymentSettingPage,
    ResetPinPage,
    PrintLayoutComponent,
    GenericSearchComponent
  ],
  entryComponents: [],
  imports: [
    FormsModule,
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    IonicModule.forRoot(),
    IonicStorageModule.forRoot(),
    AppRoutingModule,
    ReactiveFormsModule,
    SharedModule,
    CommonModule
  ],
  providers: [
    StatusBar,
    SplashScreen,
    ScreenOrientation,
    AuthenticationService,
    LocalDataService,
    PatientService,
    Printer,
    PrintService,
    AuthGuard,
    PredictionFilterPipe,
    { provide: RouteReuseStrategy, useClass: IonicRouteStrategy },
    { provide: HTTP_INTERCEPTORS, useClass: ResponseInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: RequestInterceptor, multi: true},
    {
      provide: ErrorHandler,
      useValue: Sentry.createErrorHandler({
        logErrors: !environment.production // log errors only in development which will console errors
      }),
    },
    {
      provide: Sentry.TraceService,
      deps: [Router],
    },
    {
      provide: APP_INITIALIZER,
      useFactory: () => () => {},
      deps: [Sentry.TraceService],
      multi: true,
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
