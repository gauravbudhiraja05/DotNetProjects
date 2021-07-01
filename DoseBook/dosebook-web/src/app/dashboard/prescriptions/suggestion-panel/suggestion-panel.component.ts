import { Component, EventEmitter, Input, OnChanges, OnInit, Output } from '@angular/core';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, map, switchMap, tap } from 'rxjs/operators';
import { ClassificationResult, ClassificationType } from 'src/app/models/document.model';
import { GenericSearchResponse } from 'src/app/models/http/generic-search.response';
import { Test, MedicineDose, Advice } from 'src/app/models/medicine-dose.model';
import { Prescription } from 'src/app/models/prescription.model';
import { PredictionFilterPipe } from 'src/app/pipes/prediction-filter.pipe';
import { MedicineClassifierService } from 'src/app/services/common/medicine-classifier.service';
import { NotificationService } from 'src/app/services/common/notification.service';
import { MedicineService } from 'src/app/services/http/medicine.service';
import { Stemmer } from 'src/app/utils/stemmer';

@Component({
  selector: 'app-suggestion-panel',
  templateUrl: './suggestion-panel.component.html',
  styleUrls: ['./suggestion-panel.component.scss'],
})
export class SuggestionPanelComponent implements OnInit {

  @Input()
  prx$: BehaviorSubject<Prescription> = new BehaviorSubject<Prescription>(null);

  allClassifications: ClassificationResult[] = [];

  @Output()
  selected = new EventEmitter<ClassificationResult>();

  selectedTab = 'MEDICINE';

  medicines: ClassificationResult[] = [];
  tests: ClassificationResult[] = [];
  advices: ClassificationResult[] = [];

  _prx: Prescription;

  stemmer: Stemmer;

  constructor(private medicineService: MedicineService,
              private notification: NotificationService,
              private classificationService: MedicineClassifierService,
              private filterPipe: PredictionFilterPipe) {
    this.stemmer = new Stemmer();
  }

  ngOnInit() {

    this.prx$.subscribe((prx) => {
      this._prx = prx;
      if (prx?.problem) {
        this.allClassifications = this.classificationService.getSuggestion(this.stemmer.tokenizeAndStem(prx.problem, false));
        this.filter(this.selectedTab);
      }

    });
  }

  get prx(): Observable<Prescription> {
    return this.prx$.asObservable();
  }

  set setPrx(prescription: Prescription) {
    this.prx$.next(prescription);
  }

  filter(type: string = ClassificationType.MEDICINE) {
    this.selectedTab = type;

    this.medicines.length = 0;
    this.advices.length = 0;
    this.tests.length = 0;

    const items = this.filterPipe.transform(this.allClassifications, this._prx);

    items.forEach(result => {
      if (result.classification.type === ClassificationType.MEDICINE) {
        this.medicines.push(result);
      } else if (result.classification.type === ClassificationType.ADVICE) {
        this.advices.push(result);
      } else {
        this.tests.push(result);
      }
    });
  }

  select(result: ClassificationResult) {
    const resultToEmit = { ...result };
    this.selected.emit(resultToEmit);
  }

}
