import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Constants } from 'src/app/helpers/constants';
import { Document } from 'src/app/models/document.model';

@Injectable({
  providedIn: 'root'
})
export class MedicineClassifierDataService {

  constructor(private http: HttpClient) { }

  // loadClassifiers(): Observable<Document[]> {
  //   return this.http.get<Document[]>(Constants.LOAD_MEDICINE_CLASSIFICATION);
  // }
}
