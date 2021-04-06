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
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
