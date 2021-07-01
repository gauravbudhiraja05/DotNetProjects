import { Injectable, OnDestroy } from '@angular/core';
import { interval, Subscription } from 'rxjs';
import { ClassificationResult, ClassificationType, Document } from 'src/app/models/document.model';
import { Test, MedicineDose, Advice } from 'src/app/models/medicine-dose.model';
import { environment } from 'src/environments/environment';
import { BayesClassifier } from '../../utils/bayes-classifier';
import { MedicineClassifierDataService } from '../http/medicine-classifier-data.service';
import * as _ from 'lodash';
import { Prescription } from 'src/app/models/prescription.model';
import { Stemmer } from 'src/app/utils/stemmer';

@Injectable({
    providedIn: 'root'
})
export class MedicineClassifierService {

    classifier: BayesClassifier;

    medicines: Document[] = [];

    stemmer: Stemmer;

    constructor(private httpMedicineClassifier: MedicineClassifierDataService) {
        this.classifier = new BayesClassifier();
        this.stemmer = new Stemmer();
        // this.httpMedicineClassifier.loadClassifiers().subscribe((medicines) => {
        //     this.medicines = medicines;
        //     this.classifier.train(medicines);
        // });
    }

    async syncData(): Promise<void> {
        // send data to server when n
    }

    getSuggestion(text: string | string[]): ClassificationResult[] {
        return this.classifier.getClassifications(text);
    }

    async parseSuggestions(prx: Prescription): Promise<void> {
        const medicines = prx.medicines;
        const words = this.stemmer.tokenizeAndStem(prx.problem, false);
        const updatedSuggestions = [];
        if (medicines) {
            for (const medicine of medicines) {
                await this.addMedPredictions(medicine, words);
            }
        }

        const tests = prx.tests;
        if (tests) {
            for (const test of tests) {
                await this.addTestPredictions(test, words);
            }
        }

        const advices = prx.suggestsions;
        if (advices) {
            for (const advice of advices) {
                await this.addAdvicePredictions(advice, words);
            }
        }

        await this.syncData();
    }

    async addTestPredictions(test: Test, words: string[]): Promise<Document[]> {
        const testDocs = this.classifier._toDocument().filter((doc) => doc.result.classification.type === ClassificationType.TEST);
        return testDocs;
    }

    async addAdvicePredictions(advice: Advice, words: string[]): Promise<Document[]> {
        const adviceDocs = this.classifier._toDocument().filter((doc) => doc.result.classification.type === ClassificationType.ADVICE);

        let found = false;
        let shouldRefresh = false;

        for (const doc of adviceDocs) {

            if (_.isEqual(doc.result.classification.advice.description, doc.description)) {
                // check if it is saved for all descriptions including age
                found = true;
                // check
                if (!_.isEqual(doc.description, words)) {
                    doc.description = words;
                    shouldRefresh = true;
                    break;
                }
            }
        }

        return adviceDocs;
    }

    async addMedPredictions(medicine: MedicineDose, words: string[]): Promise<Document[]> {
        // TODO if not already added in doc

        const medicineDocs = this.classifier._toDocument().filter((doc) => doc.result.classification.type === ClassificationType.MEDICINE);

        let found = false;
        let shouldRefresh = false;

        for (const doc of medicineDocs) {

            if (_.isEqual(doc.result.classification.dose, medicine)) {
                // check if it is saved for all descriptions including age
                found = true;
                // check
                if (!_.isEqual(doc.description, words)) {
                    doc.description = words;
                    shouldRefresh = true;
                    break;
                }
            }
        }

        if (!found) {
            this.classifier.addNew({
                label: medicine.medicineName,
                result: {
                    value: 0,
                    classification: {
                        type: ClassificationType.MEDICINE,
                        dose: medicine
                    }
                },
                description: words
            });
        }
        return medicineDocs;
    }
}
