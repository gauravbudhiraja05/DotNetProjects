import { Doctor } from './doctor.model';

export interface Record {

    record: string;

    addedBy: Doctor;

    addedAt: Date;

    updatedBy: Doctor;

    updatedAt: Date;

    type: RecordType;

}

export enum RecordType {

    HISTORY = 'history',
    DIAGNOSTIC = 'diagnostic'

}
