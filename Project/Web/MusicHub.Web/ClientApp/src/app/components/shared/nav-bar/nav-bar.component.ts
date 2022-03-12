import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { AuthService } from 'src/app/core/services/auth.service';
import { Router } from '@angular/router';
import { FollowService } from '../../../core/services/follow.service';
import Follow from '../models/follow';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent implements OnInit {
  @Output() sidenavToggle = new EventEmitter<void>();
  followeds: Follow[];
  isAuth: boolean = false;
  isAdmin: boolean = false;
  role: string = "";
  username: string = "";

  constructor(
    private router: Router,
    public authService: AuthService,
    public followService: FollowService
  ) {
    this.isAdmin = authService.isAdmin;
    this.isAuth = authService.isAuth;
    this.role = authService.role;
    this.username = authService.username;
    this.authService.isAuthChanged.subscribe(() => {
      this.isAuth = this.authService.isAuth;
      this.isAdmin = this.authService.isAdmin;
      this.role = this.authService.role;
      this.username = authService.username;
    })
  }

  ngOnInit(): void {
    this.followService.allFollowed().subscribe(data => {
      this.followeds = data;
    });
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
}
