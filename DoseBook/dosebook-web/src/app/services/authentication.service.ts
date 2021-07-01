import { Injectable } from '@angular/core';
import { Storage } from '@ionic/storage';
import { ToastController, NavController } from '@ionic/angular';
import { BehaviorSubject } from 'rxjs';
import { UserService } from './http/user.service';
import { Constants } from '../helpers/constants';
import { LoginResponse } from '../models/http/login-response';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  authState$: BehaviorSubject<LoginResponse> = new BehaviorSubject(null);

  token: string = null;

  constructor(private navCtrl: NavController,
              private storage: Storage,
              private userService: UserService,
              public toastController: ToastController) {
    this.authState$.subscribe((loginResponse) => {
      if (loginResponse && loginResponse.token) {
        this.token = loginResponse.token;
      } else {
        this.token = null;
      }
    });

    this.storage.get(Constants.USER_STORGAE_KEY).then((response: LoginResponse) => {
      if (response && response.token) {
        this.authState$.next(response);
      }
    });
  }

  async isLoggedIn(): Promise<boolean> {
    const response =  await this.storage.get(Constants.USER_STORGAE_KEY);
    return response && response.token;
  }

  async login(email: string, password: string): Promise<boolean> {
    try {
      const response = await this.userService.login(email, password);
      await this.storage.set(Constants.USER_STORGAE_KEY, response);
      this.authState$.next(response);
      return true;
    } catch (error) {
      this.authState$.next(null);
      return false;
    }
  }

  async logout(): Promise<void> {
    await this.storage.remove(Constants.USER_STORGAE_KEY);
    this.authState$.next(null);
    this.navCtrl.navigateRoot('login');
  }

}
