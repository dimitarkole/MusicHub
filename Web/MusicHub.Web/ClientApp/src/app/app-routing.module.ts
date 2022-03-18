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
import { ProfileUserComponent } from './profiles/components/profile-user/profile-user.component';
import { RolesListComponent } from './roles/components/roles-list/roles-list.component';
import { UsersListComponent } from './users/components/users-list/users-list.component';
import { UserDetailsComponent } from './users/components/user-details/user-details.component';
import { ConfirmEmailComponent } from './auth/confirm-email/confirm-email.component';
import { ResendEmailConfirmComponent } from './auth/resend-email-confirm/resend-email-confirm.component';
import { ForgottenPasswordComponent } from './auth/forgotten-password/forgotten-password.component';
import { CategoryAnalysisComponent } from './categories/components/category-analysis/category-analysis.component';
import { RoleAnalysisComponent } from './roles/components/role-analysis/role-analysis.component';
import { LikedMusicsListComponent } from './musics/components/liked-musics-list/liked-musics-list.component';
import { PlayedMusicsListsComponent } from './musics/components/played-musics-lists/played-musics-lists.component';
import { PlaylistResolver } from './core/resolvers/playlist.resolver';
import { PlaylistPlayComponent } from './playlists/components/playlist-play/playlist-play.component';
import { AlbumsListComponent } from './albums/components/albums-list/albums-list.component';
import { AlbumsOwnListComponent } from './albums/components/albums-own-list/albums-own-list.component';
import { AlbumPlayComponent } from './albums/components/album-play/album-play.component';
import { PlayLaterMusicsListComponent } from './play-later-musics/components/play-later-musics-list/play-later-musics-list.component';
import { OwnMusicsAnalysisComponent } from './musics/components/own-musics-analysis/own-musics-analysis.component';

const routes: Routes = [
  { path: '', pathMatch: 'full', component: HomeComponent },
  { path: 'signup', component: SignUpComponent },
  { path: 'signin', component: SignInComponent },
  { path: 'resend-confirm-email', component: ResendEmailConfirmComponent },
  { path: 'forgottenPassword', component: ForgottenPasswordComponent },
  {
    path: 'confirm-email/:id',
    component: ConfirmEmailComponent,
    resolve:
    {
      user: UserResolver
    }
  },
  {
    path: 'categories', children: [
      {
        path: '',
        component: CategoriesListComponent,
        canActivate: [PermissionsGuard],
        data: withPermission(PermissionType.CategoryAdministrator)
      },
      {
        path: 'analysis',
        component: CategoryAnalysisComponent,
        canActivate: [PermissionsGuard],
        data: withPermission(PermissionType.CategoryAdministrator)
      }
    ],
  },
  {
    path: 'playlists', children: [
      { path: '', component: PlaylistsListComponent },
      {
        path: 'own',
        component: PlaylistsOwnListComponent,
        canActivate: [PermissionsGuard],
        data: withPermission(PermissionType.PlaylistCreator)
      },
      {
        path: 'play/:id',
        component: PlaylistPlayComponent,
        resolve:
        {
          playlist: PlaylistResolver
        },
      },
    ]
  },
  {
    path: 'albums', children: [
      { path: '', component: AlbumsListComponent },
      {
        path: 'own',
        component: AlbumsOwnListComponent,
        canActivate: [PermissionsGuard],
        data: withPermission(PermissionType.PlaylistCreator)
      },
      {
        path: 'play/:id',
        component: AlbumPlayComponent,
        resolve:
        {
          playlist: PlaylistResolver
        },
      },
    ]
  },
  {
    path: 'musics', children: [
      { path: 'liked', component: LikedMusicsListComponent, canActivate: [AuthorizeGuard] },
      { path: 'history', component: PlayedMusicsListsComponent, canActivate: [AuthorizeGuard] },
      {
        path: 'own',
        component: MusicsOwnListComponent,
        canActivate: [PermissionsGuard],
        data: withPermission(PermissionType.MusicCreator)
      },
      { path: 'own/analysis', component: OwnMusicsAnalysisComponent, canActivate: [AuthorizeGuard] },
      {
        path: 'create',
        component: MusicCreateComponent,
        canActivate: [PermissionsGuard],
        data: withPermission(PermissionType.MusicCreator)
      },
      {
        path: 'edit/:id',
        component: MusicCreateComponent,
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
    path: 'playLater', children: [
      { path: '', component: PlayLaterMusicsListComponent, canActivate: [AuthorizeGuard] },
    ]
  },
  {
    path: 'license', canActivate: [AuthorizeGuard], children: [
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
    ]
  },
  {
    path: 'myProfile', children: [
      { path: '', component: ProfileUserComponent, canActivate: [AuthorizeGuard] },
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
  {
    path: 'role', children: [
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
    ]
  },
  {
    path: 'members', children: [
      {
        path: '',
        component: UsersListComponent,
      },
      {
        path: 'all',
        component: UsersListComponent,
      }]
  },
  {
    path: 'users', children: [
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
      },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

/*



import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { UserResolver } from './core/resolvers/user.resolver';
import { SignUpComponent } from './auth/sign-up/sign-up.component';
import { SignInComponent } from './auth/sign-in/sign-in.component';
import { AuthorizeGuard } from './core/guards/auth.guard';
import { UsersListComponent } from './users/components/users-list/users-list.component';
import { ConfirmEmailComponent } from './auth/confirm-email/confirm-email.component';
import { ResendEmailConfirmComponent } from './auth/resend-email-confirm/resend-email-confirm.component';
import { ForgottenPasswordComponent } from './auth/forgotten-password/forgotten-password.component';
import { MusicPlayComponent } from './musics/components/music-play/music-play.component';
import { MusicResolver } from './core/resolvers/music.resolver';
import { PlaylistResolver } from './core/resolvers/playlist.resolver';

const routes: Routes = [
  { path: '', pathMatch: 'full', component: HomeComponent },
  { path: 'signup', component: SignUpComponent },
  { path: 'signin', component: SignInComponent },
  { path: 'resend-confirm-email', component: ResendEmailConfirmComponent },
  { path: 'forgottenPassword', component: ForgottenPasswordComponent },
  {
    path: 'confirm-email/:id',
    component: ConfirmEmailComponent,
    resolve:
    {
      user: UserResolver
    }
  },
  {
    path: 'categories', loadChildren: () => import('./categories/categories.module').then(m => m.CategoriesModule)
  },
  {
    path: 'playlists', loadChildren: () => import('./playlists/playlists.module').then(m => m.PlaylistsModule)
  },
  {
    path: 'playlists/play/:id',
    component: MusicPlayComponent,
    resolve:
    {
      music: PlaylistResolver
    },
  },
  {
    path: 'musics', loadChildren: () => import('./musics/musics.module').then(m => m.MusicsModule)
  },
  /*
  {
    path: 'license', canActivate: [AuthorizeGuard], loadChildren: () => import('./licenses/licenses.module').then(m => m.LicensesModule)
  },
  {
    path: 'profile', loadChildren: () => import('./profiles/profile.module').then(m => m.ProfileModule)
  },
  {
    path: 'role', loadChildren: () => import('./roles/roles.module').then(m => m.RolesModule)
  },
  {
    path: 'members',
    children: [
      {
        path: '',
        component: UsersListComponent,
      },
      {
        path: 'all',
        component: UsersListComponent,
      }]
  },
  {
    path: 'users', loadChildren: () => import('./shared/shared.module').then(m => m.SharedModule)
  }*
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { scrollPositionRestoration: 'top' })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
*/
