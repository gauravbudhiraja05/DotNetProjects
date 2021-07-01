export interface ServiceResponse<T> {

    code: number;
    message: string;
    data: T;
}
