import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { IonicModule } from '@ionic/angular';

import { PaymentSettingPage } from './payment-setting.page';

describe('PaymentSettingPage', () => {
  let component: PaymentSettingPage;
  let fixture: ComponentFixture<PaymentSettingPage>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PaymentSettingPage ],
      imports: [IonicModule.forRoot()]
    }).compileComponents();

    fixture = TestBed.createComponent(PaymentSettingPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
