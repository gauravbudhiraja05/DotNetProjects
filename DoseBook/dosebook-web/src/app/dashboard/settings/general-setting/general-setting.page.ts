import { Component, OnInit } from '@angular/core';
import { ModalController } from '@ionic/angular';
import { ResetPinPage } from './reset-pin/reset-pin.page';

@Component({
  selector: 'app-general-setting',
  templateUrl: './general-setting.page.html',
  styleUrls: ['./general-setting.page.scss'],
})
export class GeneralSettingPage implements OnInit {

  constructor(private modalCtrl: ModalController) { }

  ngOnInit() {
  }

  async resetPinModal() {
      const modal = await this.modalCtrl.create({
        component: ResetPinPage
      });

      await modal.present();
  }

}
