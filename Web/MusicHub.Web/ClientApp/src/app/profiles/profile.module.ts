import { NgModule } from '@angular/core';
import { CommonModule, DatePipe, HashLocationStrategy, LocationStrategy } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AuthorizeGuard } from '../core/guards/auth.guard';
import { PermissionType } from '../shared/models/permissionType';
import { PermissionsGuard } from '../core/guards/permissions.guard';
import { withPermission } from '../core/guards/with-permission';
import { MatSidenavModule } from '@angular/material/sidenav';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutingModule } from '../app-routing.module';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { LayoutModule } from '@angular/cdk/layout';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { JwtInterceptor } from '../core/interceptors/jwt.interceptor';
import { BaseUrlInterceptor } from '../core/interceptors/base-url.interceptor';
import { AppComponent } from '../app.component';
import { ProfileChangePasswordComponent } from './components/profile-change-password/profile-change-password.component';
import { ProfileEditComponent } from './components/profile-edit/profile-edit.component';
import { ProfileUserComponent } from './components/profile-user/profile-user.component';
import { NgxChartsModule } from '@swimlane/ngx-charts';
import { MatDialogModule } from '@angular/material/dialog';
import { SharedModule } from '../shared/shared.module';
import { MusicsModule } from '../musics/musics.module';
import { PlaylistsModule } from '../playlists/playlists.module';
import { ProfilesRoutingModule } from './profiles-routing.module';
import { UserIpRuleListComponent } from './components/user-ip-rule-list/user-ip-rule-list.component';
import { UserIpRuleCreateComponent } from './components/user-ip-rule-create/user-ip-rule-create.component';
import { UserIpRuleEditComponent } from './components/user-ip-rule-edit/user-ip-rule-edit.component';

@NgModule({
  declarations: [
    ProfileChangePasswordComponent,
    ProfileEditComponent,
    ProfileUserComponent,
    UserIpRuleListComponent,
    UserIpRuleCreateComponent,
    UserIpRuleEditComponent
  ],
  exports: [
    ProfileChangePasswordComponent,
    ProfileEditComponent,
    ProfileUserComponent,
    UserIpRuleCreateComponent
  ],
  entryComponents: [
    ProfileEditComponent,
    ProfileChangePasswordComponent,
    UserIpRuleCreateComponent,
  ],
  imports: [
    BrowserModule,
    NgxChartsModule,
    BrowserAnimationsModule,
    MatSidenavModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
    NgbModule,
    LayoutModule,
    MatToolbarModule,
    MatButtonModule,
    MatIconModule,
    MatListModule,
    MatDialogModule,
    FormsModule,
    SharedModule,
    MusicsModule,
    PlaylistsModule,
    ProfilesRoutingModule
  ],
  providers: [
    DatePipe,
    { provide: LocationStrategy, useClass: HashLocationStrategy },
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    //{ provide: HTTP_INTERCEPTORS, useClass: BaseUrlInterceptor, multi: true },
  ],
  bootstrap: [AppComponent]
})
export class ProfileModule { }
