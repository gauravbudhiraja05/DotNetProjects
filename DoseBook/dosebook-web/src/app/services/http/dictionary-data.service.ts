import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Constants } from 'src/app/helpers/constants';
import { PredictionItem } from 'src/app/models/prediction-item.model';

@Injectable({
  providedIn: 'root'
})
export class DictionaryDataService {

  constructor(private http: HttpClient) { }

  loadDictionary(text: string): Observable<string[]> {
    return this.http.get<string[]>(`${Constants.DICTIONARY_API}?q=${text}`).pipe(map(result => {
        return this.handleResponse(result); 
    }));
  }

  handleResponse(result): string[] {
    let data: string[] = [];
    result.forEach(resultItem => {
      data.push(resultItem.words);
    });
    return data;
  }

}
