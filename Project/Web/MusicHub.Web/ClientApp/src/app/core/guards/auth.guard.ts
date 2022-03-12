import { 
  CanActivate, 
  ActivatedRouteSnapshot, 
  RouterStateSnapshot, 
  Router 
} from '@angular/router';
import { Injectable } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class AuthorizeGuard implements CanActivate {
  constructor(
    private router: Router,
    private authenticationService: AuthService) {
  }

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean> | boolean {
    let currentUrl: string = state.url;
    let token = this.authenticationService.getToken();
    if (!token) {
      this.navigateToLogin(currentUrl);
      return false;
    }
    else {
      let validateRequest = this.authenticationService.isLoggedIn();
      validateRequest.subscribe(
        response => {
          if (!response) {
            this.authenticationService.logout();
            this.authenticationService.logout();
            this.navigateToLogin(currentUrl);
          }
        },
        error => {
          this.authenticationService.logout();
          this.authenticationService.logout();
          this.navigateToLogin(currentUrl);
        })

      return validateRequest;
    }
  }

  private navigateToLogin(currentUrl: string) {
    this.router.navigate(['login'], {
      queryParams: {
        returnUrl: currentUrl
      }
    });
  }
}
