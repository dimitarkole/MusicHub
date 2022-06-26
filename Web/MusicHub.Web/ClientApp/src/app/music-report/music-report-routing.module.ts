import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthorizeGuard } from '../core/guards/auth.guard';
import { PermissionsGuard } from '../core/guards/permissions.guard';
import { withPermission } from '../core/guards/with-permission';
import { LicenseResolver } from '../core/resolvers/license.resolver';
import { MusicResolver } from '../core/resolvers/music.resolver';
import { PermissionType } from '../shared/models/permissionType';

const routes: Routes = [
  
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class LicensesRoutingModule { }
