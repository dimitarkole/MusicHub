import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthorizeGuard } from '../core/guards/auth.guard';
import { PermissionsGuard } from '../core/guards/permissions.guard';
import { withPermission } from '../core/guards/with-permission';
import { LicenseResolver } from '../core/resolvers/license.resolver';
import { MusicResolver } from '../core/resolvers/music.resolver';
import { PermissionType } from '../shared/models/permissionType';
import { LicenseCreateComponent } from './components/license-create/license-create.component';
import { LicenseListComponent } from './components/license-list/license-list.component';
import { LicenseOwnListComponent } from './components/license-own-list/license-own-list.component';
import { LicenseRequestComponent } from './components/license-request/license-request.component';

const routes: Routes = [
  {
    path: '',
    component: LicenseOwnListComponent,
    canActivate: [PermissionsGuard],
    data: withPermission(PermissionType.LicenseCreator)
  },
  {
    path: 'request/:id',
    component: LicenseRequestComponent,
    resolve:
    {
      license: LicenseResolver
    },
    canActivate: [PermissionsGuard],
    data: withPermission(PermissionType.LicenseManager)
  },
  {
    path: 'own',
    component: LicenseOwnListComponent,
    canActivate: [PermissionsGuard],
    data: withPermission(PermissionType.LicenseCreator)
  },
  {
    path: 'all',
    component: LicenseListComponent,
    canActivate: [PermissionsGuard],
    data: withPermission(PermissionType.LicenseManager)
  },
  {
    path: 'create',
    component: LicenseCreateComponent,
    canActivate: [PermissionsGuard],
    data: withPermission(PermissionType.LicenseCreator)
  },
  /*{
    path: 'view/:id',
    component: LicenseDetailsComponent,
    resolve:
    {
      license: LicenseResolver
    }
  },*/
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class LicensesRoutingModule { }
