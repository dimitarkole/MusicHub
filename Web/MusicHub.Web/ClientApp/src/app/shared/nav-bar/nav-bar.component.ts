import { Component, OnInit, EventEmitter, Output, OnChanges, SimpleChanges } from '@angular/core';
import { AuthService } from 'src/app/core/services/auth.service';
import { Router } from '@angular/router';
import { Permission } from '../models/permission';
import { PermissionType } from '../models/permissionType';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent implements OnInit {
  @Output() sidenavToggle = new EventEmitter<void>();
  permitions: Permission[];
  isAuth: boolean = false;
  isAdmin: boolean = false;
  role: string = "";
  username: string = "";
  token: string = "";

  constructor(
    private router: Router,
    public authService: AuthService
  ) {
    this.isAdmin = authService.isAdmin;
    this.isAuth = authService.isAuth;
    this.role = authService.role;
    this.username = authService.username;
    this.token = authService.getToken();
    this.authService.isAuthChanged.subscribe(() => {
      this.isAuth = this.authService.isAuth;
      this.isAdmin = this.authService.isAdmin;
      this.role = this.authService.role;
      this.username = authService.username;
    });
  }

  ngOnInit(): void {
    if (this.isAuth) {
      this.authService.getPermissions().subscribe(data => {
        this.permitions = data;
      });
    }
  }

  toggleSidenav() {
    this.sidenavToggle.emit();
  }

  logout() {
    this.authService.logout();
    this.authService.initializeAuthenticationState();
    this.router.navigate(['/']);
    location.reload();
  }

  hasPermition(type: string): boolean {
    return this.permitions.some(p => p.type == type);
  }
}
