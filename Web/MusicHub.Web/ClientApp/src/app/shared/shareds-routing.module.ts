import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthorizeGuard } from '../core/guards/auth.guard';
import { PermissionsGuard } from '../core/guards/permissions.guard';
import { withPermission } from '../core/guards/with-permission';
import { MusicResolver } from '../core/resolvers/music.resolver';
import { UserResolver } from '../core/resolvers/user.resolver';
import { PermissionType } from '../shared/models/permissionType';
import { UserDetailsComponent } from '../users/components/user-details/user-details.component';
import { UsersListComponent } from '../users/components/users-list/users-list.component';

const routes: Routes = [
  {
    path: '',
    component: UsersListComponent,
    canActivate: [PermissionsGuard],
    data: withPermission(PermissionType.UserAdministration)
  },
  {
    path: 'all',
    component: UsersListComponent,
    canActivate: [PermissionsGuard],
    data: withPermission(PermissionType.UserAdministration)
  },
  {
    path: 'details/:id',
    component: UserDetailsComponent,
    resolve:
    {
      user: UserResolver
    },
    canActivate: [PermissionsGuard],
    data: withPermission(PermissionType.ManageUserPermissions)
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SharedsRoutingModule { }
