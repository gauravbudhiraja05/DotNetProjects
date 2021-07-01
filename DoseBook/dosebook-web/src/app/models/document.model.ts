import { Test, MedicineDose, Advice } from './medicine-dose.model';

export interface Document {
    label: string;
    result: ClassificationResult;
    description: string[];
}

export interface ClassificationResult {
    value: number;
    classification: ClassificationData;
}

export interface ClassificationData {
    type: ClassificationType;
    dose?: MedicineDose;
    test?: Test;
    advice?: Advice;
}

export enum ClassificationType {
    TEST = 'TEST',
    MEDICINE = 'MEDICINE',
    ADVICE = 'ADVICE'
}
