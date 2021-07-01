import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { empty, Observable, of, Subject } from 'rxjs';
import { catchError, debounceTime, distinctUntilChanged, switchMap, tap } from 'rxjs/operators';
import { ClassificationType } from 'src/app/models/document.model';
import { GenericSearchResponse } from 'src/app/models/http/generic-search.response';
import { Test, MedicineDose, Advice } from 'src/app/models/medicine-dose.model';
import { MedicineService } from 'src/app/services/http/medicine.service';
@Component({
  selector: 'app-generic-search',
  templateUrl: './generic-search.component.html',
  styleUrls: ['./generic-search.component.scss'],
})
export class GenericSearchComponent implements OnInit {

  genericSearchTextChanged = new Subject<string>();
  genericSearchText = '';

  searching = false;

  searchResponse$: Observable<GenericSearchResponse>;

  @Output()
  selected: EventEmitter<MedicineDose | Advice | Test> = new EventEmitter();

  @Input()
  searchType: ClassificationType;

  constructor(private medicineService: MedicineService) { }

  ngOnInit() {
    this.searchResponse$ = this.genericSearchTextChanged.pipe(
      tap(_ => this.searching = true),
      debounceTime(300),
      distinctUntilChanged(),
      switchMap((term: string) => {
        if (term) {
          return this.medicineService.searchGeneric(term, this.searchType);
        } else {
          this.searching = false;
          return of({} as GenericSearchResponse);
        }
      }),
      tap(_ => this.searching = false)
    ).pipe(
      catchError((error, caught) => {
        this.searching = false;
        return caught;
      }));
  }

  async inputChanged() {
    this.genericSearchTextChanged.next(this.genericSearchText);
  }


  clear() {
    this.genericSearchText = '';
  }

  medicineSelected(medicine: any) {
    const medicineDose: MedicineDose = { ...medicine };
    medicineDose.medicineName = medicine.medicine_name;
    medicineDose.doseUnit = medicine.dose_unit;
    this.selected.emit(medicineDose);
  }

  testSelected(test: any) {
    const testToSave: Test = {
      testName: test.test_name,
      testId: test.id
    };
    this.selected.emit(testToSave);
  }

  adviceSelected(advice: any) {
    const suggestion: Advice = { ...advice };
    this.selected.emit(suggestion);
  }

}
