import { Injectable } from '@angular/core';
import { ToastController } from '@ionic/angular';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {

  constructor(private toastController: ToastController) { }

  async success(text: string): Promise<void> {
    const toast = await this.toastController.create({
      message: text,
      duration: 2000,
      color: 'primary'
    });
    return await toast.present();
  }

  async error(text: string): Promise<void> {
    const toast = await this.toastController.create({
      message: text,
      duration: 2000,
      color: 'red'
    });
    return await toast.present();
  }
}
