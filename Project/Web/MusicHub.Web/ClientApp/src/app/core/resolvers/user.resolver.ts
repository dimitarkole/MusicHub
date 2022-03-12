import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { ProfileService } from '../services/profile.service';
import User from '../../components/shared/models/user';

@Injectable({
    providedIn: 'root'
})
export class UserResolver implements Resolve<User> {
  constructor(private profileService: ProfileService) { }

  resolve(route: ActivatedRouteSnapshot): Observable<User> {
      let id = route.params['id'];
      return this.profileService.getById(id);
    }
}
