import { Component, OnInit } from '@angular/core';
import { HeaderNavService } from 'src/app/services/header-nav.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
})
export class HeaderComponent implements OnInit {

  selectedItem = 'patients';

  constructor(private headerNavService: HeaderNavService) { }

  ngOnInit() {}

  menuItemClicked(itemName: string) {
    this.selectedItem = itemName;
    this.headerNavService.menuItemClicked(itemName);
  }

}
