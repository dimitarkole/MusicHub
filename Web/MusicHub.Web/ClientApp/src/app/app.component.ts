import { Component } from '@angular/core';
import { AuthService } from './core/services/auth.service';
import { NavBarComponent } from './shared/nav-bar/nav-bar.component'
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  isLoggedIn: boolean;
  title: string;
  constructor(private authService: AuthService){
    this.authService.initializeAuthenticationState();
  }
}
