import { Injectable } from '@angular/core';
import { HttpClient, } from '@angular/common/http';
import { Constants } from 'src/app/helpers/constants';
import { Observable } from 'rxjs';
import { LoginResponse } from 'src/app/models/http/login-response';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }

  login(email: string, password: string): Promise<LoginResponse> {
    return this.http.post<LoginResponse>(Constants.USER_LOGIN, {
      email,
      password
    }).toPromise();
  }

  // getClinics(): Observable<DoctorClinic[]> {
  //   return this.http.get<DoctorClinic[]>(Constants.GET_CLINICS);
  // }
}
