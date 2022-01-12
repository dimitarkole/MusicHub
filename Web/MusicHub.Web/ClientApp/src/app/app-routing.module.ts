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
    path: 'musics', loadChildren: () => import('./musics/musics.module').then(m => m.MusicsModule)
  },
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
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
