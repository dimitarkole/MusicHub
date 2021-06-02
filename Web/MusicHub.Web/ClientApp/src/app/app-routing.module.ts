import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { UserResolver } from './core/resolvers/user.resolver';
import { SignUpComponent } from './auth/sign-up/sign-up.component';
import { SignInComponent } from './auth/sign-in/sign-in.component';
import { AuthorizeGuard } from './core/guards/auth.guard';
import { CategoriesListComponent } from './categories/components/categories-list/categories-list.component';
import { PermissionsGuard } from './core/guards/permissions.guard';
import { withPermission } from './core/guards/with-permission';
import { PermissionType } from './shared/models/permissionType';
import { PlaylistsListComponent } from './playlists/components/playlists-list/playlists-list.component';
import { PlaylistsOwnListComponent } from './playlists/components/playlists-own-list/playlists-own-list.component';
import { MusicsOwnListComponent } from './musics/components/musics-own-list/musics-own-list.component';
import { MusicCreateComponent } from './musics/components/music-create/music-create.component';
import { MusicEditComponent } from './musics/components/music-edit/music-edit.component';
import { MusicResolver } from './core/resolvers/music.resolver';
import { MusicDetailsComponent } from './musics/components/music-details/music-details.component';
import { MusicPlayComponent } from './musics/components/music-play/music-play.component';
import { LicenseListComponent } from './licenses/components/license-list/license-list.component';
import { LicenseRequestComponent } from './licenses/components/license-request/license-request.component';
import { LicenseResolver } from './core/resolvers/license.resolver';
import { LicenseCreateComponent } from './licenses/components/license-create/license-create.component';
import { LicenseDetailsComponent } from './licenses/components/license-details/license-details.component';
import { LicenseOwnListComponent } from './licenses/components/license-own-list/license-own-list.component';
import { ProfileOwnComponent } from './profiles/components/profile-own/profile-own.component';
import { ProfileUserComponent } from './profiles/components/profile-user/profile-user.component';

const routes: Routes = [
  { path: '', pathMatch: 'full', component: HomeComponent },
  { path: 'signup', component: SignUpComponent },
  { path: 'signin', component: SignInComponent },
  {
    path: 'categories', component: CategoriesListComponent
  },
  {
    path: 'playlists', children: [
      { path: '', component: PlaylistsListComponent },
      { path: 'own', component: PlaylistsOwnListComponent, canActivate: [PermissionsGuard], data: withPermission(PermissionType.PlaylistCreator) },
    ]
  },
  {
    path: 'musics', children: [
      { path: 'own', component: MusicsOwnListComponent, canActivate: [PermissionsGuard], data: withPermission(PermissionType.MusicCreator) },
      { path: 'create', component: MusicCreateComponent, canActivate: [PermissionsGuard], data: withPermission(PermissionType.MusicCreator) },
      {
        path: 'edit/:id',
        component: MusicEditComponent,
        resolve:
        {
          music: MusicResolver
        },
        canActivate: [AuthorizeGuard],
        data: withPermission(PermissionType.MusicCreator)
      },
      {
        path: 'play/:id',
        component: MusicPlayComponent,
        resolve:
        {
          music: MusicResolver
        },
      },
    ]
  },
  {
    path: 'license', canActivate: [AuthorizeGuard], children: [
      { path: '', component: LicenseOwnListComponent },
      {
        path: 'request/:id',
        component: LicenseRequestComponent,
        resolve:
        {
          license: LicenseResolver
        }
      },
      { path: 'own', component: LicenseOwnListComponent },
      { path: 'all', component: LicenseListComponent },
      { path: 'create', component: LicenseCreateComponent },
      /*{
        path: 'view/:id',
        component: LicenseDetailsComponent,
        resolve:
        {
          license: LicenseResolver
        }
      },*/
    ]
  },
  {
    path: 'myProfile', children: [
      { path: '', component: ProfileOwnComponent, canActivate: [AuthorizeGuard] },
    ]
  },
  {
    path: 'profile', children: [
      {
        path: 'view/:id',
        component: ProfileUserComponent,
        resolve:
        {
          user: UserResolver
        }
      },
    ]
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
