import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import License from '../../licenses/models/license';
import { LicenseService } from '../../licenses/services/license.service';

@Injectable({
  providedIn: 'root'
})
export class LicenseResolver implements Resolve<License> {
  constructor(private licenseService: LicenseService) { }

  resolve(route: ActivatedRouteSnapshot): Observable<License> {
    let id = route.params['id'];
    return this.licenseService.getById(id);
  }
}

