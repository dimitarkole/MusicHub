import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { SignupComponent } from './auth/signup/signup.component';
import { LoginComponent } from './auth/login/login.component';
import { MyProfileComponent } from './components/profile/my-profile/my-profile.component';
import { UserResolver } from './core/resolvers/user.resolver';
import { PlaylistResolver } from './core/resolvers/playlist.resolver';
import { ListenedSongHistoryAllComponent } from './components/listenedSongsHistory/listened-song-history-all/listened-song-history-all.component';
import { CategoryListComponent } from './components/category/category-list/category-list.component';
import { CategoryCreateComponent } from './components/category/category-create/category-create.component';
import { PlaylistListAllComponent } from './components/playlist/playlist-list-all/playlist-list-all.component';
import { PlayComponent } from './components/song/play/play.component';
import { SongListComponent } from './components/song/song-list/song-list.component';
import { SongCreateComponent } from './components/song/song-create/song-create.component';
import { SongEditComponent } from './components/song/song-edit/song-edit.component';
import { PlaylistListComponent } from './components/playlist/playlist-list/playlist-list.component';
import { PlaylistPlayComponent } from './components/playlist/playlist-play/playlist-play.component';
import { UserProfileComponent } from './components/profile/user-profile/user-profile.component';
import { SongResolver } from './core/resolvers/song.resolver';
import { ForgottenPasswordComponent } from './auth/forgotten-password/forgotten-password.component';
import { UserAgreementComponent } from './components/legal/user-agreement/user-agreement.component';
import { PrivacyPolicyComponent } from './components/legal/privacy-policy/privacy-policy.component';
import { AuthorizeGuard } from './core/guards/auth.guard';
import { LikedSongsListComponent } from './components/song/liked-songs-list/liked-songs-list.component';
import { LicenseListComponent } from './components/license/license-list/license-list.component';
import { LicenseAllListComponent } from './components/license/license-all-list/license-all-list.component';
import { LicenseDetailComponent } from './components/license/license-detail/license-detail.component';
import { LicenseResolver } from './core/resolvers/license.resolver';
import { LicenseCreateComponent } from './components/license/license-create/license-create.component';
import { LicenseRequestComponent } from './components/license/license-request/license-request.component';

const routes: Routes = [
  { path: '', pathMatch: 'full', component: HomeComponent },
  {
    path: 'legal', children: [
      { path: 'user-agreement', component: UserAgreementComponent },
      { path: 'privacy-policy', component: PrivacyPolicyComponent }
    ]
  },
  {
    path: 'home', children: [
      { path: '', component: HomeComponent },
      { path: 'likedSongs', component: LikedSongsListComponent, canActivate: [AuthorizeGuard]  },
      {
        path: 'play/:id',
        component: PlayComponent,
        resolve:
        {
          song: SongResolver
        }
      },
      { path: 'playlists', component: PlaylistListAllComponent },
      { path: 'listenedSongs', component: ListenedSongHistoryAllComponent, canActivate: [AuthorizeGuard] },
    ]
  },
  { path: 'signup', component: SignupComponent },
  { path: 'login', component: LoginComponent },
  { path: 'forgottenPassword', component: ForgottenPasswordComponent},
  {
    path: 'category', children: [
      { path: '', component: CategoryListComponent, canActivate: [AuthorizeGuard]  },
      { path: 'all', component: CategoryListComponent, canActivate: [AuthorizeGuard]  },
      { path: 'create', component: CategoryCreateComponent, canActivate: [AuthorizeGuard]  },
    ]
  },
  {
    path: 'song', children: [
      { path: '', component: SongListComponent, canActivate: [AuthorizeGuard]  },
      { path: 'own', component: SongListComponent, canActivate: [AuthorizeGuard]  },
      { path: 'create', component: SongCreateComponent, canActivate: [AuthorizeGuard] },
      {
        path: 'edit/:id',
        component: SongEditComponent,
        resolve:
        {
          song: SongResolver
        },
        canActivate: [AuthorizeGuard] 
      },
    ]
  },
  {
    path: 'myProfile', children: [
      { path: '', component: MyProfileComponent, canActivate: [AuthorizeGuard]  },
    ]
  },
  {
    path: 'profile', children: [
      {
        path: 'view/:id',
        component: UserProfileComponent,
        resolve:
        {
          user: UserResolver
        }},
    ]
  },
  {
    path: 'playlist', children: [
      { path: 'own', component: PlaylistListComponent, canActivate: [AuthorizeGuard]  },
      {
        path: 'play/:id',
        component: PlaylistPlayComponent,
        resolve:
        {
          playlist: PlaylistResolver
        }
      }, 
    ]
  },
  {
    path: 'license', canActivate: [AuthorizeGuard], children: [
      { path: '', component: LicenseListComponent },
      {
        path: 'request/:id',
        component: LicenseRequestComponent,
        resolve:
        {
          license: LicenseResolver
        } },
      { path: 'own', component: LicenseListComponent },
      { path: 'all', component: LicenseAllListComponent },
      { path: 'create', component: LicenseCreateComponent},
      {
        path: 'view/:id',
        component: LicenseDetailComponent,
        resolve:
        {
          license: LicenseResolver
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
