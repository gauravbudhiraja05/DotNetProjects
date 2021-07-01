import { PredictionItemFactory } from '../../helpers/prediction-item.factory';
import { Injectable, OnDestroy } from '@angular/core';
import { Observable, interval, Subscription } from 'rxjs';
import { environment } from 'src/environments/environment';
import { PredictionItem } from 'src/app/models/prediction-item.model';
import { PredictionDataService } from '../http/prediction-data.service';

@Injectable({
    providedIn: 'root'
})
export class WordPredictorService implements OnDestroy {

    dictionary = {};

    _lastPredictionInput = null;

    _lastPredictions: PredictionItem[] = null;

    _lastChosenWord: string = null;

    INBETWEEN_CHARS_REGEX = '[\\s\\.\\?!,]';
    PHRASE_END_CHARS_REGEX = '[\\.\\?!,]';
    SENTENCE_END_CHARS_REGEX = '[\\.\\?!]';

    PREDICT_METHOD_COMPLETE_WORD = 'PREDICT_METHOD_COMPLETE_WORD';
    PREDICT_METHOD_NEXT_WORD = 'PREDICT_METHOD_NEXT_WORD';

    refreshDataSubscription: Subscription;

    constructor(private predictionDataService: PredictionDataService) {
        this.predictionDataService.loadDictionary().subscribe((data: PredictionItem[]) => {
            this.initDictionary(data);
        });
    }

    ngOnDestroy() {
    }

    async saveDictionary(): Promise<void> {
        const words = await this.predictionDataService.saveDictionary(this.toDictionary());
        this.initDictionary(words);
    }

    initDictionary(items: PredictionItem[]) {
        this.dictionary = {};
        items.forEach((item) => {
            this.dictionary[item.w] = item;
        });
        console.log('Updated dictionary == ', this.dictionary);
    }

    toDictionary(): PredictionItem[] {
        const dictionaryData = JSON.parse(JSON.stringify(this.dictionary));
        console.log(dictionaryData);
        return Object.keys(dictionaryData).map((key) =>  {
            return dictionaryData[key];
        });
    }

    toJSON(): string {
        const copy = JSON.parse(JSON.stringify(this.dictionary));
        Object.keys(copy).forEach(key => {
            delete copy[key].w;
        });
        return JSON.stringify(copy);
    }

    addWords(words: string[]) {
        if (words.length === 0) {
            throw new Error('words to add must be an array with at least one element.');
        }

        words.forEach(word => {
            this.addWord(word);
        });
    }

    addWord(word: string, rank?: any) {
        if (!word) {
            return;
        }
        if (!this.dictionary[word]) {
            this.dictionary[word] = PredictionItemFactory.createItem(word, rank);
        }
    }

    deleteWord(word, ignoreCase: boolean = false) {
        Object.keys(this.dictionary).forEach(dictWord => {
            const equalWord = ignoreCase ? word.toUpperCase() === dictWord.toUpperCase() : word === dictWord;
            if (equalWord) {
                delete this.dictionary[dictWord];
            } else {
                const dictElement = this.dictionary[dictWord];
                Object.keys(dictElement.t).forEach(transistionWord => {
                    const equalTransitionWord = ignoreCase ? word.toUpperCase() === transistionWord.toUpperCase() : word === transistionWord;
                    if (equalTransitionWord) {
                        delete dictElement.t[transistionWord];
                    }
                });
            }
        });
    }

    contains(word: string, matchCase: boolean = false) {
        if (matchCase) {
            return !!this.dictionary[word];
        } else {
            return !!this.getBestFittingItem(word);
        }
    }

    isLastWordCompleted(text) {
        return new RegExp(this.INBETWEEN_CHARS_REGEX).test(text[text.length - 1]);
    }

    learnFromInput(input) {
        if (this.isLastWordCompleted(input)) {
            const chosenWord = this.getLastWord(input, 2);
            const previousWord = this.getLastWord(input, 3);
            if (chosenWord && chosenWord !== this._lastChosenWord) {
                this._lastChosenWord = chosenWord;
                this.learn(chosenWord, previousWord, true);
                return true;
            }
        }
        return false;
    }

    applyPrediction(input: string, chosenPrediction: string, options?: any) {
        options = options || {};
        const addToDictionary = options.addToDictionary || null;

        // if last word is not complete or complete last word flag is given
        const shouldCompleteLastWord = options.shouldCompleteLastWord !== undefined ?
            options.shouldCompleteLastWord : !this.isLastWordCompleted(input);

        const dontLearn = true;
        const lastWord = this.getLastWord(input, 0);
        const preLastWord = this.getLastWord(input, 2);
        let temp = shouldCompleteLastWord ? input.substring(0, input.lastIndexOf(lastWord)) : input;
        if (temp.length > 0 && (!this.isLastWordCompleted(temp) || new RegExp(this.PHRASE_END_CHARS_REGEX).test(temp[temp.length - 1]))) {
            temp += ' ';
        }
        if (!dontLearn) {
            this.learn(chosenPrediction, !shouldCompleteLastWord ? lastWord : preLastWord, addToDictionary);
        }
        return temp + chosenPrediction + ' ';
    }

    getLastWord(text, index?: number) {
        index = index || 1;
        const words = text.trim().split(new RegExp(this.INBETWEEN_CHARS_REGEX)).filter(word => !!word);
        const returnWord = words[words.length - index] || '';
        return returnWord.replace(new RegExp(this.INBETWEEN_CHARS_REGEX, 'g'), '');
    }

    predictCompleteWord(input: string, options?: any): PredictionItem[] {
        input = input || '';
        options = options || {}; // maxPredictions, predictionMinDepth, predictionMaxDepth, compareFn
        const possiblePredictions: PredictionItem[] = [];
        Object.keys(this.dictionary).forEach(key => {
            if (key.toLowerCase().indexOf(input.toLowerCase()) === 0) {
                possiblePredictions.push(this.dictionary[key]);
            }
        });
        if (possiblePredictions.length === 0 && input.length > 1) {
            let result = null;
            if (this._lastPredictionInput && this._lastPredictions && input.indexOf(this._lastPredictionInput) === 0) {
                result = this._lastPredictions;
            } else {
                result = this.predictCompleteWord(input.substring(0, input.length - 1), options);
            }
            result.forEach(element => {
                element.fuzzyMatch = true;
            });
            return result;
        }
        this._lastPredictionInput = input;
        this._lastPredictions = possiblePredictions.map(element => {
            return {
                w: element.w,
                f: element.f,
                r: element.r
            };
        });
        return this._lastPredictions;
    }

    learn(chosenWord: string, previousWord: string, addIfNotExisting: boolean) {
        if (!chosenWord || (!this.contains(chosenWord) && !addIfNotExisting)) {
            return;
        }
        if (addIfNotExisting && chosenWord && !this.contains(chosenWord)) {
            this.addWord(chosenWord);
        }
        if (addIfNotExisting && previousWord && !this.contains(previousWord)) {
            this.addWord(previousWord);
        }
        const previousWordItem = this.getBestFittingItem(previousWord);
        const chosenWordItem = this.getBestFittingItem(chosenWord);
        chosenWordItem.f++;
        if (previousWordItem && previousWordItem.t) {
            if (previousWordItem.t[chosenWordItem.w]) {
                previousWordItem.t[chosenWordItem.w]++;
            } else {
                previousWordItem.t[chosenWordItem.w] = 1;
            }
        }
    }

    predict(input: string, options?: any): string[] {
        return this.predictInternal(input, options);
    }

    private predictInternal(input: string, options?: any, predictType?: string): string[] {
        let predictions: PredictionItem[] = [];
        options = options || {};
        options.maxPredictions = options.maxPredictions || options.maxPredicitons || 10;
        options.applyToInput = options.applyToInput || false;
        predictions = predictions.concat(this.predictCompleteWord(this.getLastWord(input), options));
        predictions.sort((a, b) => {
            if (a.fuzzyMatch !== b.fuzzyMatch) {
                return a.fuzzyMatch ? 1 : -1;
            }
            if (a.f !== b.f) {
                return (a.f < b.f) ? 1 : -1;
            }
            if (a.r !== b.r) {
                if (a.r && b.r === undefined) { return -1; }
                if (b.r && a.r === undefined) { return 1; }
                return (a.r < b.r) ? -1 : 1;
            }
            return 0;
        });
        const returnArray: string[] = [];
        for (let i = 0; i < predictions.length && returnArray.length < options.maxPredictions; i++) {
            if (returnArray.indexOf(predictions[i].w) === -1) { // de-duplicate
                returnArray.push(predictions[i].w);
            }
        }
        return returnArray;
    }

    getDictItemsAnyCase(word): PredictionItem[] {
        if (!word) {
            return [];
        }
        const items: PredictionItem[] = [];
        if (this.dictionary[word]) { items.push(this.dictionary[word]); }
        if (this.dictionary[word.toLowerCase()] && items.indexOf(this.dictionary[word.toLowerCase()]) === -1) { items.push(this.dictionary[word.toLowerCase()]); }
        if (this.dictionary[word.toUpperCase()] && items.indexOf(this.dictionary[word.toUpperCase()]) === -1) { items.push(this.dictionary[word.toUpperCase()]); }
        if (this.dictionary[this.capitalize(word)] && items.indexOf(this.dictionary[this.capitalize(word)]) === -1) { items.push(this.dictionary[this.capitalize(word)]); }
        return items;
    }

    getBestFittingItem(word: string) {
        const items = this.getDictItemsAnyCase(word);
        return items.length > 0 ? items[0] : null;
    }

    capitalize(data: string) {
        return data.charAt(0).toUpperCase() + data.slice(1);
    }

}
