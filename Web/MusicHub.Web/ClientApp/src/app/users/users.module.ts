import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { UsersListComponent } from './components/users-list/users-list.component';
import { PermissionsGuard } from '../core/guards/permissions.guard';
import { withPermission } from '../core/guards/with-permission';
import { PermissionType } from '../shared/models/permissionType';
import { UsersListTemplateComponent } from './components/users-list-template/users-list-template.component';
import { UserDetailsComponent } from './components/user-details/user-details.component';
import { UserAvatarComponent } from './components/user-avatar/user-avatar.component';

const routes: Routes = [
  { 
    path: '', 
    component: UsersListComponent, 
    canActivate:[PermissionsGuard], 
    data: withPermission(PermissionType.UserAdministration) 
  },
	// { 
  //   path: 'create/ad', 
  //   component: UsercreateComponent, 
  //   canActivate:[PermissionsGuard], 
  //   data: withPermission(PermissionType.UserAdministration) 
  // },
]

@NgModule({
  declarations: [
    UsersListComponent,
    UsersListTemplateComponent,
    UserDetailsComponent,
    UserAvatarComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forChild(routes),
  ]
})
export class UsersModule { }
