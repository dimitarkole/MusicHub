import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { PermissionType } from '../../shared/models/permissionType';
import { AuthService } from '../services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class PermissionsGuard implements CanActivate {
  constructor(
    private userService: AuthService,
    private router: Router) { }

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): boolean | Observable<boolean> {
    let currentUrl: string = state.url;
    let token = this.userService.getToken();
    if (!token) {
      this.navigateToLogin(currentUrl);
      return false;
    }

    let permissionType = route.data.permissionType as PermissionType;
    let validationRequest = this.userService.hasPermission(permissionType);
    validationRequest.subscribe(
      hasAccess => {
        if (!hasAccess) {
          this.userService.logout();
          this.navigateToLogin(currentUrl);
        }
      },
      error => {
        this.userService.logout();
        this.navigateToLogin(currentUrl);
      }
    );

    return validationRequest;
  }

  private navigateToLogin(currentUrl: string) {
    this.router.navigate(['login'], {
      queryParams: {
        returnUrl: currentUrl
      }
    });
  }
}
