import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { WordPredictorService } from 'src/app/services/common/word-predictor.service';
import { Stemmer } from 'src/app/utils/stemmer';

@Component({
  selector: 'app-word-prediction-panel',
  templateUrl: './word-prediction-panel.component.html',
  styleUrls: ['./word-prediction-panel.component.scss'],
})
export class WordPredictionPanelComponent implements OnInit {

  private inputText$: BehaviorSubject<string> = new BehaviorSubject<string>(null);

  @Output()
  selected: EventEmitter<string> = new EventEmitter<string>();

  @Input()
  learn = true;

  predictions: string[] = [];

  stemmer: Stemmer;

  constructor(private wordPredictionService: WordPredictorService) {
    this.stemmer = new Stemmer();
  }

  ngOnInit() {
    this.inputText$.subscribe((text) => {
      this.filterPrediction(text);
    });
  }

  get inputText(): Observable<string> {
    return this.inputText$.asObservable();
  }

  set changeInputText(text: string) {
    this.inputText$.next(text);
  }

  select(prediction: string) {
    this.selected.emit(prediction);
  }

  /**
   * Filters predictions not to include already selected prediction and words from input text
   *
   * @param prediction SelectedPrediction
   */
  private filterPrediction(text: string) {
    this.predictions.length = 0;
    if (text) {
      // words to exclude from predictions which already exist in input text, We are using stemmer to remove stop words
      const wordsToExclude: string[] = this.stemmer.tokenizeAndStem(text, false);

      const predictions = this.wordPredictionService.predict(text).filter(el => {
        return wordsToExclude.indexOf( el.toLowerCase() ) < 0;
      });
      this.predictions.push(...predictions);
    }
  }

}
