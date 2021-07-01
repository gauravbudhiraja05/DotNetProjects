import { Record } from './medical-record';
export interface Patient {

    id?: number;

    name: string;

    dob: Date;

    gender: string;

    mobile: string;

    email?: string;

    history?: Record[]; // can be of type histiry or allergic info

}
