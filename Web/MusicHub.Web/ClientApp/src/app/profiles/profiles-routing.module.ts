import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthorizeGuard } from '../core/guards/auth.guard';
import { PermissionsGuard } from '../core/guards/permissions.guard';
import { withPermission } from '../core/guards/with-permission';
import { MusicResolver } from '../core/resolvers/music.resolver';
import { UserResolver } from '../core/resolvers/user.resolver';
import { PlaylistsListComponent } from '../playlists/components/playlists-list/playlists-list.component';
import { PlaylistsOwnListComponent } from '../playlists/components/playlists-own-list/playlists-own-list.component';
import { PermissionType } from '../shared/models/permissionType';
import { UserDetailsComponent } from '../users/components/user-details/user-details.component';
import { UsersListComponent } from '../users/components/users-list/users-list.component';
import { ProfileUserComponent } from './components/profile-user/profile-user.component';
import { UserIpRuleListComponent } from './components/user-ip-rule-list/user-ip-rule-list.component';

const routes: Routes = [
  { path: '', component: PlaylistsListComponent },
  {
    path: 'own',
    component: PlaylistsOwnListComponent,
    canActivate: [PermissionsGuard],
    data: withPermission(PermissionType.PlaylistCreator)
  },
  {
    path: 'userIpRules',
    component: UserIpRuleListComponent
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProfilesRoutingModule { }
