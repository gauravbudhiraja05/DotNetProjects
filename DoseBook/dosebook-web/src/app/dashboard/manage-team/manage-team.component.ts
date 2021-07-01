import { Component, OnInit } from '@angular/core';
import { SubUser } from 'src/app/models/sub-user.model';

@Component({
  selector: 'app-manage-team',
  templateUrl: './manage-team.component.html',
  styleUrls: ['./manage-team.component.scss'],
})
export class ManageTeamComponent implements OnInit {

  members: SubUser[] = [{
    email: 'test@test.com',
    createdAt: new Date(),
    lastLoginAt: new Date(),
    mobile: '9999999',
    name: 'Test',
    status: 'ACtive',
    updatedAt: new Date()
  }];

  constructor() { }

  ngOnInit() {}

}
