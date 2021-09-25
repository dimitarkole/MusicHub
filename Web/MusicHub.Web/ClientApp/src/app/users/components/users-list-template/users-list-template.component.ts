import { Component, Input, OnInit } from '@angular/core';
import { AuthService } from '../../../core/services/auth.service';
import { Permission } from '../../../shared/models/permission';
import User from '../../../shared/models/user';

@Component({
  selector: 'app-users-list-template',
  templateUrl: './users-list-template.component.html',
  styleUrls: ['./users-list-template.component.css']
})
export class UsersListTemplateComponent implements OnInit {
  @Input() users: User[] = [];
  permitions: Permission[];
 
  constructor(
    private authService: AuthService) {
  }

  ngOnInit() {
    this.getUserData();
  }

  hasPermition(type: string): boolean {
    return this.permitions == null ? false : this.permitions.some(p => p.type == type);
  }

  private getUserData() {
    var permission: Permission = { type: "User", description: "", id: 1 }
    this.permitions = [permission];
    if (this.authService.isAuth) {
      this.authService.getPermissions().subscribe(p => {
        this.permitions = p;
      });
    }
  }
}
