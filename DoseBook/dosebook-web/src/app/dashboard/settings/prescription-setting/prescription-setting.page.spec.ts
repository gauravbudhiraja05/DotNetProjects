import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { IonicModule } from '@ionic/angular';

import { PrescriptionSettingPage } from './prescription-setting.page';

describe('PrescriptionSettingPage', () => {
  let component: PrescriptionSettingPage;
  let fixture: ComponentFixture<PrescriptionSettingPage>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PrescriptionSettingPage ],
      imports: [IonicModule.forRoot()]
    }).compileComponents();

    fixture = TestBed.createComponent(PrescriptionSettingPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
