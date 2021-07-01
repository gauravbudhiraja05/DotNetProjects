import { PrxAdviceResponse } from './prx-advice.response';
import { PrxMedicineResponse } from './prx-medicine.response';
import { PrxTestResponse } from './prx-test.response';

export interface PrescriptionResponse {
    id: number;
    patient_id: number;
    doctor_id: number;
    problem: string;
    next_visit_due: string;
    tests: PrxTestResponse[];
    advices: PrxAdviceResponse[];
    medicines: PrxMedicineResponse[];
    created_at: string;
    updated_at: string;
}
