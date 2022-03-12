import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import License from '../../components/shared/models/license';
import { LicenseService } from '../services/license.service';

@Injectable({
    providedIn: 'root'
})
export class LicenseResolver implements Resolve<License> {
  constructor(private licenseSercie: LicenseService) { }

  resolve(route: ActivatedRouteSnapshot): Observable<License> {
        let id = route.params['id'];
    return this.licenseSercie.getById(id);
   }
}
