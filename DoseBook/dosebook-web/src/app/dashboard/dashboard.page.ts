import { Component, OnDestroy, OnInit } from '@angular/core';
import { HeaderNavService } from '../services/header-nav.service';
import { Router } from '@angular/router';
import { UserService } from '../services/http/user.service';
import { AlertController, NavController } from '@ionic/angular';
import { Observable, Subscription } from 'rxjs';
import { AuthenticationService } from '../services/authentication.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.page.html',
  styleUrls: ['./dashboard.page.scss'],
})
export class DashboardPage implements OnInit {

  subscription: Subscription;

  isActive = true;

  menuLinks = [
    { name: 'patients', link: '' },
    { name: 'prescriptions', link: 'prescriptions' },
    { name: 'view_all_prescriptions', link: 'view-all-prescriptions'},
    { name: 'view_payments', link: 'view-payments'},
    { name: 'manage_team', link: 'manage-team'},
    { name: 'settings', link: 'settings'},
    { name: 'logout', link: 'logout'}
  ];

  constructor(private headerNavService: HeaderNavService,
              private authService: AuthenticationService,
              private navController: NavController,
              private alterController: AlertController,
              private router: Router) { }

  ngOnInit() {

    this.subscription = this.headerNavService.selectedMenu.asObservable().subscribe((itemName: string) => {
      const itemLink = this.menuLinks.find(menuLink => menuLink.name === itemName);
      console.log(itemLink);
      if (itemLink) {
        if (itemLink.name === 'logout') {
          this.presentLogoutAlert();
        } else if (itemLink) { // if item is valid
          this.router.navigate([`/dashboard/${itemLink.link}`]);
        }
      }
    });

  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  async presentLogoutAlert() {
    const alert = await this.alterController.create({
      message: 'Do you want to logout?',
      buttons: [
        {
          text: 'Cancel',
          role: 'cancel',
          handler: () => {
            console.log('Cancel clicked');
          }
        },
        {
          text: 'Yes',
          handler: () => {
            this.authService.logout();
          }
        }
      ]
    });
    alert.present();
  }

}
