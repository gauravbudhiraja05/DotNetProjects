import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { IonicModule } from '@ionic/angular';

import { ViewPrescriptionPage } from './view-prescription.page';

describe('ViewPrescriptionPage', () => {
  let component: ViewPrescriptionPage;
  let fixture: ComponentFixture<ViewPrescriptionPage>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ViewPrescriptionPage ],
      imports: [IonicModule.forRoot()]
    }).compileComponents();

    fixture = TestBed.createComponent(ViewPrescriptionPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
