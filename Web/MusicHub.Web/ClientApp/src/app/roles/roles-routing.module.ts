import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthorizeGuard } from '../core/guards/auth.guard';
import { PermissionsGuard } from '../core/guards/permissions.guard';
import { withPermission } from '../core/guards/with-permission';
import { MusicResolver } from '../core/resolvers/music.resolver';
import { PermissionType } from '../shared/models/permissionType';
import { RoleAnalysisComponent } from './components/role-analysis/role-analysis.component';
import { RolesListComponent } from './components/roles-list/roles-list.component';

const routes: Routes = [
  {
    path: '',
    component: RolesListComponent,
    canActivate: [PermissionsGuard],
    data: withPermission(PermissionType.ManageUserPermissions)
  },
  {
    path: 'all',
    component: RolesListComponent,
    canActivate: [PermissionsGuard],
    data: withPermission(PermissionType.ManageUserPermissions)
  },
  {
    path: 'analysis',
    component: RoleAnalysisComponent,
    canActivate: [PermissionsGuard],
    data: withPermission(PermissionType.ManageUserPermissions)
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class RolesRoutingModule { }
