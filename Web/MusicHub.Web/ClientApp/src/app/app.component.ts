import { Component, OnInit } from '@angular/core';
import { environment } from '../environments/environment';
import { AuthService } from './core/services/auth.service';
import { NavBarComponent } from './shared/nav-bar/nav-bar.component'
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  isLoggedIn: boolean;
  title: string;
  cookieMessage: string="This web site uses cookies to ensure you get the best experience on our websitte.";
  cookieDismiss: string= "Got id";
  cookieLinkText: string= "Lear";

  constructor(private authService: AuthService){
    this.authService.initializeAuthenticationState();
  }

    ngOnInit(): void {
      let cc = window as any;
      cc.cookieconsent.initialise({
        palette: {
          popup: {
            background: "#2457d7"
          },
          button: {
            background: "#ff1a4b",
            text: "#164969"
          }
        },
        theme: "classic",
        content: {
          message: this.cookieMessage,
          dismiss: this.cookieDismiss,
          link: this.cookieLinkText,
          href: "/privacy"
        }
      });
    }
}
