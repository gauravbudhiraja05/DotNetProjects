import { ClassificationResult, ClassificationType, Document } from '../models/document.model';
import { Stemmer } from './stemmer';

export class BayesClassifier {


    stemmer: Stemmer;

    constructor() {
        this.stemmer = new Stemmer();
        this.train([]);
    }

    /*
     * The stemmer provides tokenization methods.
     * It breaks the doc into words (tokens) and takes the
     * stem of each word. A stem is a form to which affixes
     * can be attached, aka root word.
     */
    /*
     * A collection of added documents
     * Each document is an object containing the class, and array of stemmed words.
     */
    private alldocs: Document[] = [];

    /*
     * Index of last added document.
     */
    lastAdded = 0;

    /*
     * A map of all class features.
     */
    features = {};

    /*
     * A map containing each class and associated features.
     * Each class has a map containing a feature index and the count of feature appearances for that class.
     */
    classFeatures = {};

    /*
     * Keep track of how many features in each class.
     */
    classTotals = {};

    /*
     * Number of examples trained
     */
    totalDataItems = 1;

    /* Additive smoothing to eliminate zeros when summing features,
     * in cases where no features are found in the document.
     * Used as a fail-safe to always return a class.
     * http://en.wikipedia.org/wiki/Additive_smoothing
     */
    smoothing = 1;

    /**
     * docToFeatures
     *
     * @desc
     * Returns an array with 1's or 0 for each feature in document
     * A 1 if feature is in document
     * A 0 if feature is not in document
     *
     * @param {string | array} doc - document
     * @return {number[]} features map
     */
    docToFeatures(features: string | string[]): number[] {

        const featuresMapInCurrentItem: number[] = [];

        if (this._isString(features)) {
            features = this.stemmer.tokenizeAndStem(features, '');
        } else if (this._isArray(features)) {

            // Add token (feature) to features map.. double check if any key is missing in global features map
            for (const feature of features) {
                this.features[feature] = 1;
            }
        }

        // check what all features are there in current item doc and mark them 1 and 0 accordingly
        for (const feature of Object.keys(this.features)) {
            // tslint:disable-next-line: no-bitwise
            featuresMapInCurrentItem.push(Number(!!~features.indexOf(feature))); // if feature is found in array
        }

        return featuresMapInCurrentItem;
    }

    /**
     * train
     * @desc train the classifier on the added documents.
     * @return {object} - Bayes classifier instance
     */
    train(allDocs: Document[]): void {

        const localMedicinesData: Document[] = [
            {
                label: 'Crocin 500',
                description: [
                    'fever',
                    'age_40_50'
                ],
                result: {
                    value: 0,
                    classification: {
                        type: ClassificationType.MEDICINE,
                        dose: {
                            directions: 'BEFORE MEAL',
                            dose: 1,
                            doseUnit: 'tablet',
                            duration: '2 Weeks',
                            frequency: '1-1-0',
                            composition: 'Paracetamol',
                            id: 1,
                            medicineName: 'Crocin 500'
                        },

                    }
                }
            },
            {
                label: 'Flexson 500',
                description: [
                    'fever',
                    'pain',
                    'age_40_50'
                ],
                result: {
                    value: 0,
                    classification: {
                        type: ClassificationType.MEDICINE,
                        dose: {
                            directions: 'AFTER MEAL',
                            dose: 1,
                            doseUnit: 'tablet',
                            duration: '2 Weeks',
                            frequency: '1-1-1',
                            composition: 'Paracetamol + Ibrufen',
                            id: 1,
                            medicineName: 'Flexson 500'
                        },

                    }
                }
            },
            {
                label: 'Fasting Sugar',
                description: [
                    'sugar'
                ],
                result: {
                    value: 0,
                    classification: {
                        type: ClassificationType.TEST,
                        test: {
                            testId: 1,
                            testName: 'Fasting Sugar'
                        }

                    }
                }
            }
        ];

        if (localMedicinesData) {
            allDocs = localMedicinesData;
        }

        // assign all features
        for (const doc of allDocs) {
            this.assignFeatures(doc);
        }

        for (const doc of allDocs) {
            this.addNew(doc);
        }
        this.alldocs = allDocs;
    }

    refreshDocs(docs: Document[]) {
        this.alldocs = docs;
        this.train(this.alldocs);
    }

    addNew(doc: Document) {
        const featureMapping = this.docToFeatures(doc.description);
        this.addDataItem(featureMapping, doc);
    }

    /**
     * train
     * @desc train the classifier on the added documents.
     * @return {object} - Bayes classifier instance
     */
    assignFeatures(doc: Document) {
        // Add token (feature) to features map
        // will add all items from description with value 1 { fever: 1, cough: 1, pain: 1 }
        for (const desc of doc.description) {
            this.features[desc] = 1;
        }
    }

    /**
     * addExample
     * @desc Increment the counter of each feature for each class.
     * @param {array} docFeatures
     * @param {string} label - class
     * @return {object} - Bayes classifier instance
     */
    addDataItem(featureMapping: number[], doc: Document) {

        const label = doc.label;
        if (!this.classFeatures[label]) {
            this.classFeatures[label] = {
                data: {},
                result: doc.result
            };
            this.classTotals[label] = 1;
        }

        this.totalDataItems++;

        let i = featureMapping.length;
        this.classTotals[label]++;

        while (i--) {
            if (featureMapping[i]) {
                if (this.classFeatures[label].data[i]) {
                    this.classFeatures[label].data[i]++;
                } else {
                    this.classFeatures[label].data[i] = 1 + this.smoothing;
                }
            }
        }
    }


    /**
     * probabilityOfClass
     * @param {array|string} docFeatures - document features
     * @param {string} label - class
     * @return probability;
     * @desc
     * calculate the probability of class for the document.
     *
     * Algorithm source
     * http://en.wikipedia.org/wiki/Naive_Bayes_classifier
     *
     * P(c|d) = P(c)P(d|c)
     *          ---------
     *             P(d)
     *
     * P = probability
     * c = class
     * d = document
     *
     * P(c|d) = Likelyhood(class given the document)
     * P(d|c) = Likelyhood(document given the classes).
     *     same as P(x1,x2,...,xn|c) - document `d` represented as features `x1,x2,...xn`
     * P(c) = Likelyhood(class)
     * P(d) = Likelyhood(document)
     *
     * rewritten in plain english:
     *
     * posterior = prior x likelyhood
     *             ------------------
     *                evidence
     *
     * The denominator can be dropped because it is a constant. For example,
     * if we have one document and 10 classes and only one class can classify
     * document, the probability of the document is the same.
     *
     * The final equation looks like this:
     * P(c|d) = P(c)P(d|c)
     */
    probabilityOfClass(docFeatures, label): number {
        let count = 0;
        let prob = 0;

        if (this._isArray(docFeatures)) {
            let i = docFeatures.length;

            // Iterate though each feature in document.
            while (i--) {
                // Proceed if feature collection.
                if (docFeatures[i]) {
                    /*
                     * The number of occurances of the document feature in class.
                     */
                    count = this.classFeatures[label].data[i] || this.smoothing;

                    /* This is the `P(d|c)` part of the model.
                     * How often the class occurs. We simply count the relative
                     * feature frequencies in the corpus (document body).
                     *
                     * We divide the count by the total number of features for the class,
                     * and add it to the probability total.
                     * We're using Natural Logarithm here to prevent Arithmetic Underflow
                     * http://en.wikipedia.org/wiki/Arithmetic_underflow
                     */
                    prob += Math.log(count / this.classTotals[label]);
                }
            }
        } else {
            for (const key of Object.keys(docFeatures)) {
                count = this.classFeatures[label].data[docFeatures[key]] || this.smoothing;
                prob += Math.log(count / this.classTotals[label]);
            }
        }

        /*
         * This is the `P(c)` part of the model.
         *
         * Divide the the total number of features in class by total number of all features.
         */
        const featureRatio = (this.classTotals[label] / this.totalDataItems);

        /**
         * probability of class given document = P(d|c)P(c)
         */
        prob = featureRatio * Math.exp(prob);

        return prob;
    }



    /**
     * getClassifications
     * @desc Return array of document classes their probability values.
     * @param {string} doc - document
     * @return classification ordered by highest probability.
     */
    getClassifications(doc): ClassificationResult[] {
        const result: ClassificationResult[] = [];

        const docWithLowerCase = [];

        if (this._isArray(doc)) {
            for (const docValue of doc) {
                docWithLowerCase.push(docValue.toLowerCase());
            }
            doc = docWithLowerCase;
        } else {
            doc = doc.toLowerCase();
        }

        for (const className of Object.keys(this.classFeatures)) {
            result.push({
                value: this.probabilityOfClass(this.docToFeatures(doc), className),
                classification: this.classFeatures[className].result.classification
            });
        }

        return result.sort((x, y) => {
            return y.value - x.value;
        });
    }


    /*
     * Helper utils
     */
    _isString(s) {
        return typeof (s) === 'string' || s instanceof String;
    }

    _isArray(s) {
        return Array.isArray(s);
    }

    _isObject(s) {
        return typeof (s) === 'object' || s instanceof Object;
    }

    _size(s) {
        if (this._isArray(s) || this._isString(s) || this._isObject(s)) {
            return s.length;
        }
        return 0;
    }

    _toDocument(): Document[] {
        return Object.assign([], this.alldocs);
    }

}
