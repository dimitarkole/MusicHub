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
import { BrowserModule } from '@angular/platform-browser';
import { NgxChartsModule } from '@swimlane/ngx-charts';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatButtonModule, MatDialogModule, MatIconModule, MatListModule, MatSidenavModule, MatToolbarModule } from '@angular/material';
import { AppRoutingModule } from '../app-routing.module';
import { HttpClientModule } from '@angular/common/http';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { LayoutModule } from '@angular/cdk/layout';
import { FollowsModule } from '../follow/follows.module';
import { UsersRoutingModule } from './users-routing.module';
@NgModule({
  declarations: [
    UsersListComponent,
    UsersListTemplateComponent,
    UserDetailsComponent,
    UserAvatarComponent
  ],
  imports: [
    BrowserModule,
    NgxChartsModule,
    BrowserAnimationsModule,
    MatSidenavModule,
    ReactiveFormsModule,
    HttpClientModule,
    NgbModule,
    LayoutModule,
    MatToolbarModule,
    MatButtonModule,
    MatIconModule,
    MatListModule,
    MatDialogModule,
    AppRoutingModule,
    FormsModule,
    FollowsModule,
    UsersRoutingModule
  ],
  exports: [
    UsersListComponent,
    UsersListTemplateComponent,
    UserDetailsComponent,
    UserAvatarComponent,
    BrowserModule,
  ],
  providers: [
  ]
})
export class UsersModule {
}
