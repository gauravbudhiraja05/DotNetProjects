import { TestBed } from '@angular/core/testing';

import { PredictionDataService } from './prediction-data.service';

describe('PredictionDataService', () => {
  let service: PredictionDataService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PredictionDataService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
