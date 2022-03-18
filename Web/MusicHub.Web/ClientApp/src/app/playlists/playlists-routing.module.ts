import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PermissionsGuard } from '../core/guards/permissions.guard';
import { withPermission } from '../core/guards/with-permission';
import { PermissionType } from '../shared/models/permissionType';
import { PlaylistsListComponent } from './components/playlists-list/playlists-list.component';
import { PlaylistsOwnListComponent } from './components/playlists-own-list/playlists-own-list.component';

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
