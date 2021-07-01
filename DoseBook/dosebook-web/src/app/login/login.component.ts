import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NavController, ToastController } from '@ionic/angular';
import { AuthenticationService } from '../services/authentication.service';
import { LoaderService } from '../services/common/loader.service';
import { NotificationService } from '../services/common/notification.service';
import { UserService } from '../services/http/user.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {

  loginForm: FormGroup;

  constructor(private navCtrl: NavController,
              private formBuilder: FormBuilder,
              public loader: LoaderService,
              public notificationService: NotificationService,
              private authService: AuthenticationService) {
    this.loginForm = this.formBuilder.group({
      email: ['demo@demo.com', [ Validators.required, Validators.min(4), Validators.email ]],
      password: ['123456', [ Validators.required, Validators.min(6) ]]
    });
  }

  ngOnInit() { // we can move below code to canactivate but we need this only at one place
    this.authService.authState$.subscribe((data) => {
      if (data) {
        this.navCtrl.navigateRoot('dashboard');
      }
    });
  }

  async login() {
    this.loader.show();
    if (this.loginForm.valid) {
      try {
        const success = await this.authService.login(this.loginForm.value.email, this.loginForm.value.password);
        this.loader.hide();
        if (success) {
          await this.notificationService.success('Login Successful');
          this.navCtrl.navigateRoot('dashboard');
        } else {
          await this.notificationService.error('Invalid Credentials');
        }
      } catch (error) {
        this.loader.hide();
        await this.notificationService.error('Server error');
      }

    }
  }

}
