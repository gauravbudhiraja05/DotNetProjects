import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { IonicModule } from '@ionic/angular';

import { ShowMedicalHistoryPage } from './show-medical-history.page';

describe('ShowMedicalHistoryPage', () => {
  let component: ShowMedicalHistoryPage;
  let fixture: ComponentFixture<ShowMedicalHistoryPage>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ShowMedicalHistoryPage ],
      imports: [IonicModule.forRoot()]
    }).compileComponents();

    fixture = TestBed.createComponent(ShowMedicalHistoryPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
