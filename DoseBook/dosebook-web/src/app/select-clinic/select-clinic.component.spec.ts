import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { IonicModule } from '@ionic/angular';

import { SelectClinicComponent } from './select-clinic.component';

describe('SelectClinicComponent', () => {
  let component: SelectClinicComponent;
  let fixture: ComponentFixture<SelectClinicComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SelectClinicComponent ],
      imports: [IonicModule.forRoot()]
    }).compileComponents();

    fixture = TestBed.createComponent(SelectClinicComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
