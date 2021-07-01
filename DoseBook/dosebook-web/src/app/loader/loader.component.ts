import { Component, OnInit, OnDestroy } from '@angular/core';
import { LoaderState } from '../models/loader.model';
import { Subscription } from 'rxjs';
import { LoaderService } from '../services/common/loader.service';


@Component({
  selector: 'app-loader',
  templateUrl: './loader.component.html',
  styleUrls: ['./loader.component.scss'],
})
export class LoaderComponent implements OnInit, OnDestroy {

  show = false;

  private subscription: Subscription;

  constructor(private loaderService: LoaderService) {

  }

  ngOnInit() {
    this.subscription = this.loaderService.loaderSubject$.subscribe((state: boolean) => {
      this.show = state;
    });
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

}
