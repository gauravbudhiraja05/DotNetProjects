import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Constants } from 'src/app/helpers/constants';
import { PredictionItem } from 'src/app/models/prediction-item.model';

@Injectable({
  providedIn: 'root'
})
export class PredictionDataService {

  constructor(private http: HttpClient) { }

  loadDictionary(): Observable<PredictionItem[]> {
    return this.http.get<PredictionItem[]>(Constants.DICTIONARY_API);
  }

  saveDictionary(words: PredictionItem[]): Promise<PredictionItem[]> {
    return this.http.post<PredictionItem[]>(Constants.DICTIONARY_API, { words }).toPromise();
  }

}
