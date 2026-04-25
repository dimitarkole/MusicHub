import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ForgottenPasswordComponent } from './forgotten-password/forgotten-password.component';
import { ResendEmailConfirmComponent } from './resend-email-confirm/resend-email-confirm.component';
import { SignInComponent } from './sign-in/sign-in.component';
import { SignUpComponent } from './sign-up/sign-up.component';
import { UserResolver } from '../core/resolvers/user.resolver';

const routes: Routes = [
  { path: 'signup', component: SignUpComponent },
  { path: 'signin', component: SignInComponent },
  {
    path: 'confirm-email/:id',
    component: ResendEmailConfirmComponent,
    resolve:
    {
      userId: UserResolver
    },
  },
  { path: 'resend-confirm-email', component: ResendEmailConfirmComponent },
  { path: 'forgottenPassword', component: ForgottenPasswordComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AuthsRoutingModule { }
