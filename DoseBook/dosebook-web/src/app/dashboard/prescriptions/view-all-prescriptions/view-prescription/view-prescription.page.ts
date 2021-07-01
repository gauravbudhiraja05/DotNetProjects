import { Component, Input, OnInit } from '@angular/core';
import { NavigationExtras } from '@angular/router';
import { ModalController, NavController } from '@ionic/angular';
import { Prescription } from 'src/app/models/prescription.model';
import { LocalDataService } from 'src/app/services/common/local-data.service';

@Component({
  selector: 'app-view-prescription',
  templateUrl: './view-prescription.page.html',
  styleUrls: ['./view-prescription.page.scss'],
})
export class ViewPrescriptionPage implements OnInit {

  @Input()
  prescription: Prescription;

  constructor(private modalCtrl: ModalController,
              private navCtrl: NavController,
              private dataService: LocalDataService) { }

  ngOnInit() {
  }

  async dismiss() {
    this.modalCtrl.dismiss();
  }

  async copyPrx() {
    this.modalCtrl.dismiss();
    this.dataService.setPrx = this.prescription;
    this.navCtrl.navigateRoot('/dashboard/new-edit-prescription');
  }

}
