import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { IonicModule } from '@ionic/angular';

import { MedicineSettingPage } from './medicine-setting.page';

describe('MedicineSettingPage', () => {
  let component: MedicineSettingPage;
  let fixture: ComponentFixture<MedicineSettingPage>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MedicineSettingPage ],
      imports: [IonicModule.forRoot()]
    }).compileComponents();

    fixture = TestBed.createComponent(MedicineSettingPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
