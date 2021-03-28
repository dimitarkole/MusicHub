import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import User from '../../shared/models/user';
//import { ProfileService } from '../services/profile.service';

@Injectable({
    providedIn: 'root'
})
export class UserResolver implements Resolve<User> {
    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): User | Observable<User> | Promise<User> {
        throw new Error('Method not implemented.');
    }
 /* constructor(private profileService: ProfileService) { }

  resolve(route: ActivatedRouteSnapshot): Observable<User> {
      let id = route.params['id'];
      return this.profileService.getById(id);
    }*/
}
