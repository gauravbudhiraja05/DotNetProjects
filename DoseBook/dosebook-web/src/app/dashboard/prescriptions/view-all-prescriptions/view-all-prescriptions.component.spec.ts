import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { IonicModule } from '@ionic/angular';

import { ViewAllPrescriptionsComponent } from './view-all-prescriptions.component';

describe('ViewAllPrescriptionsComponent', () => {
  let component: ViewAllPrescriptionsComponent;
  let fixture: ComponentFixture<ViewAllPrescriptionsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ViewAllPrescriptionsComponent ],
      imports: [IonicModule.forRoot()]
    }).compileComponents();

    fixture = TestBed.createComponent(ViewAllPrescriptionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
