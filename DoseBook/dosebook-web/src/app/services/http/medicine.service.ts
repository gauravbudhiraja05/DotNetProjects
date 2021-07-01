import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Constants } from 'src/app/helpers/constants';
import { ClassificationType } from 'src/app/models/document.model';
import { GenericSearchResponse } from 'src/app/models/http/generic-search.response';
import { PrescriptionMeta } from 'src/app/models/http/prescription-meta.response';

@Injectable({
  providedIn: 'root'
})
export class MedicineService {


  constructor(private http: HttpClient) { }

  searchGeneric(input: string, type: string = ClassificationType.MEDICINE): Observable<GenericSearchResponse> {
    return this.http.get<GenericSearchResponse>(`${Constants.SEARCH_GENERIC}?query=${input}&type=${type}`);
  }

  loadMeta(): Observable<PrescriptionMeta[]> {
    return this.http.get<PrescriptionMeta[]>(`${Constants.LOAD_PRX_META}`);
  }
}
