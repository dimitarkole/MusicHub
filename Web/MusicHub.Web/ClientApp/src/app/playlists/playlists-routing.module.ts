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
  { path: '', component: PlaylistsListComponent },
  {
    path: 'own',
    component: PlaylistsOwnListComponent,
    canActivate: [PermissionsGuard],
    data: withPermission(PermissionType.PlaylistCreator)
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProfilesRoutingModule { }
