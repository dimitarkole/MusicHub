import { Component, Input, OnInit } from '@angular/core';
import User from '../../../shared/models/user';

@Component({
  selector: 'app-users-list-template',
  templateUrl: './users-list-template.component.html',
  styleUrls: ['./users-list-template.component.css']
})
export class UsersListTemplateComponent implements OnInit {
  @Input() users: User[] = [];

  constructor() { }

  ngOnInit() {
  }

}
