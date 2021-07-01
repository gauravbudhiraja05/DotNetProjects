import { Record } from './medical-record';
import { Patient } from './patient.model';

export interface PatientStatus {

    id?: number;

    status: Status;

    patient: Patient;

}


export enum Status {

    WAITING = 'WAITING',
    PRESCRIBED = 'PRESCRIBED'

}
