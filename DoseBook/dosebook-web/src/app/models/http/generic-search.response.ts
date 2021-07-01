import { Test, MedicineDose, Advice } from '../medicine-dose.model';

export interface GenericSearchResponse {
    'medicines': [
        {
            'id': number;
            'medicine_name': string,
            'frequency': string,
            'directions': string,
            'composition': string,
            'duration': string,
            'dose': string,
            'dose_unit': string,
            'created_at': string,
            'updated_at': string
        }
    ];
    'tests': [
        {
            'id': number,
            'test_name': string,
            'created_at': string,
            'updated_at': string
        }
    ];
    'advices': [
        {
            'id': number,
            'description': string,
            'created_at': string,
            'updated_at': string
        }
    ];
}
