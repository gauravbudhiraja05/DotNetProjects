import { environment } from 'src/environments/environment';

export class Constants {

    public static readonly BASE_URL = `${environment.api_url}`;

    // Constant Values
    public static readonly USER_STORGAE_KEY = 'USER_INFO';
    public static readonly SELECTED_PATIENT = 'SELECTED_PATIENT';
    public static readonly SELECTED_PRX = 'SELECTED_PRX';

    // User Endpoints
    public static readonly USER_LOGIN = `${Constants.BASE_URL}/doctor/auth/login`;
    public static readonly GET_CLINICS = `${Constants.BASE_URL}/doctor/clinics`;
    public static readonly MARK_PRESCRIBED = `${Constants.BASE_URL}/doctor/status`;
    public static readonly SAVE_PATIENT_HISTORY = `${Constants.BASE_URL}/doctor/add-history`;
    public static readonly DICTIONARY_API = `${Constants.BASE_URL}/doctor/dictionary`;
    public static readonly SEARCH_PATIENT = `${Constants.BASE_URL}/doctor/patient`;
    public static readonly ADD_PATIENT = `${Constants.BASE_URL}/patient`;

    public static readonly SAVE_PRESCRIPTION = `${Constants.BASE_URL}/prescription`;
    public static readonly GET_PRESCRIPTIONS = `${Constants.BASE_URL}/prescriptions`;

    public static readonly GET_CONSULTATIONS = `${Constants.BASE_URL}/doctor/consultations`;
    public static readonly CREATE_CONSULTATION = `${Constants.BASE_URL}/doctor/consultation`;

    public static readonly SEARCH_GENERIC = `${Constants.BASE_URL}/doctor/generic/search`;
    public static readonly LOAD_PRX_META = `${Constants.BASE_URL}/doctor/prescription/meta`;

    public static readonly LOAD_MEDICINE_CLASSIFICATION = `${Constants.BASE_URL}/doctor/medicine-classification`;

}

