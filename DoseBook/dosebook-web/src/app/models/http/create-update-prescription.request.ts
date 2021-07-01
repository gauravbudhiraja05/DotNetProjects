import { CreatePrxAdviceRequest } from './create-prx-advice.request';
import { CreatePrxMedicineRequest } from './create-prx-medicine.request';
import { CreatePrxTestRequest } from './create-prx-test.request';

export interface CreateOrUpdatePrxRequest {

    prescription_id?: number;
    problem: string;
    next_visit_due: string;
    tests: CreatePrxTestRequest[];
    suggestions: CreatePrxAdviceRequest[];
    medicines: CreatePrxMedicineRequest[];

}
