import { Test, MedicineDose, Advice } from './medicine-dose.model';
import { Patient } from './patient.model';

export interface PrintDataModel {

    medicines: MedicineDose[];

    patient: Patient;

    tests: Test[];

    Advice: Advice[];

}
