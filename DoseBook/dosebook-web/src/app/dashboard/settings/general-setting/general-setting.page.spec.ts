import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { IonicModule } from '@ionic/angular';

import { GeneralSettingPage } from './general-setting.page';

describe('GeneralSettingPage', () => {
  let component: GeneralSettingPage;
  let fixture: ComponentFixture<GeneralSettingPage>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GeneralSettingPage ],
      imports: [IonicModule.forRoot()]
    }).compileComponents();

    fixture = TestBed.createComponent(GeneralSettingPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
