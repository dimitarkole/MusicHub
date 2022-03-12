import { Component } from '@angular/core';
import { AuthService } from './core/services/auth.service';
import { NavBarComponent } from './components/shared/nav-bar/nav-bar.component'
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  constructor(
    //private appInsightsService: AppInsightsServiceService,
    private authService: AuthService) {
    this.authService.initializeAuthenticationState();
    //appInsightsService.config();
    //appInsightsService.logPageView('MainPage');
  }
}
