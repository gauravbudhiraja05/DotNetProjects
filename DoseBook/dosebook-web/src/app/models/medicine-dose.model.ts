export interface MedicineDose {
    id?: number;
    medicineName: string;
    frequency: string;
    directions: string;
    composition: string;
    duration: string;
    dose: number; // -1 for N/A, 1, 2, 3
    doseUnit: string; // tablet, spoon, drops, points,
}


export interface Test {
    testId?: number;
    testName: string;
}

export interface Advice {
    id?: number;
    description: string;
}

export interface DoseMeta {
    id: number;
    type: DoseMetaType;
    title: string;
}

enum DoseMetaType {

    UNIT = 'units', FREQUENCY = 'frequency', DIRECTION = 'direction', DURATION = 'duration'

}
