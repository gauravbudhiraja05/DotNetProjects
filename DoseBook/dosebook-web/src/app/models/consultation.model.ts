import { ConsultationStatus } from './enum/consultation-status.enum';
import { Patient } from './patient.model';

export interface Consultation {

    id?: number;

    patient: Patient;

    status: ConsultationStatus;

}
