import { TestBed } from '@angular/core/testing';

import { MedicineClassifierDataService } from './medicine-classifier-data.service';

describe('MedicineClassifierDataService', () => {
  let service: MedicineClassifierDataService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MedicineClassifierDataService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
