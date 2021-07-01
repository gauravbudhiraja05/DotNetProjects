import { Test, MedicineDose, Advice } from './medicine-dose.model';
import { Patient } from './patient.model';

export interface Prescription {

    id?: number;
    doctorId?: number;
    medicines?: MedicineDose[];
    tests?: Test[];
    suggestsions?: Advice[];
    problem?: string;
    patient?: Patient;
    nextVisitDue?: Date;
    created?: Date;
    updated?: Date;

}
