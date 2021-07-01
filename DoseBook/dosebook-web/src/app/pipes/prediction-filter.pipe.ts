import { Pipe, PipeTransform } from '@angular/core';
import { ClassificationResult, ClassificationType } from '../models/document.model';
import { Prescription } from '../models/prescription.model';
@Pipe({
    name: 'predictionFilter'
})
export class PredictionFilterPipe implements PipeTransform {

    transform(items: ClassificationResult[], prescription: Prescription) {

        return items.filter((item) => {

            if (prescription.medicines) {
                for (const medicine of prescription.medicines) {
                    if (item.classification.type !== ClassificationType.MEDICINE) {
                        continue;
                    }

                    if (item.classification.dose.medicineName === medicine.medicineName) {
                        return false;
                    }
                }
            }

            if (prescription.tests) {
                for (const test of prescription.tests) {
                    if (item.classification.type !== ClassificationType.TEST) {
                        continue;
                    }

                    if (item.classification.test.testName === test.testName) {
                        return false;
                    }
                }
            }

            if (prescription.suggestsions) {
                for (const suggestion of prescription.suggestsions) {
                    if (item.classification.type !== ClassificationType.ADVICE) {
                        continue;
                    }

                    if (item.classification.advice.description === suggestion.description) {
                        return false;
                    }
                }
            }

            return true;
        });
    }

}
