import { NgModule } from '@angular/core';
import { CommonModule, DatePipe, HashLocationStrategy, LocationStrategy } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { PermissionType } from '../shared/models/permissionType';
import { PermissionsGuard } from '../core/guards/permissions.guard';
import { withPermission } from '../core/guards/with-permission';
import { MatSidenavModule } from '@angular/material/sidenav';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutingModule } from '../app-routing.module';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgbActiveModal, NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { LayoutModule } from '@angular/cdk/layout';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { JwtInterceptor } from '../core/interceptors/jwt.interceptor';
import { AppComponent } from '../app.component';
import { MatDialogModule } from '@angular/material';
import { FooterComponent } from './footer/footer.component';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { NoMoreResultComponent } from './no-more-result/no-more-result.component';
import { NoSearchResultsComponent } from './no-search-results/no-search-results.component';
import { OrderByDropDownComponent } from './order-by-drop-down/order-by-drop-down.component';
import { NotificationsModule } from '../notifications/notifications.module';
import { UsersListComponent } from '../users/components/users-list/users-list.component';
import { UsersListTemplateComponent } from '../users/components/users-list-template/users-list-template.component';
import { UserDetailsComponent } from '../users/components/user-details/user-details.component';
import { UserAvatarComponent } from '../users/components/user-avatar/user-avatar.component';
import { FollowCreateComponent } from '../follow/components/follow-create/follow-create.component';
import { FollowersListComponent } from '../follow/components/followers-list/followers-list.component';
import { FollowingListComponent } from '../follow/components/following-list/following-list.component';
import { FollowingListNavBarComponent } from '../follow/components/following-list-nav-bar/following-list-nav-bar.component';
import { NotificationsListComponent } from '../notifications/components/notifications-list/notifications-list.component';
import { NotificationSpecialInfoComponent } from '../notifications/components/notification-special-info/notification-special-info.component';
import { SharedsRoutingModule } from './shareds-routing.module';
import { HomeComponent } from '../home/home.component';

@NgModule({
  declarations: [
    NavBarComponent,
    NoMoreResultComponent,
    NoSearchResultsComponent,
    OrderByDropDownComponent,
    FooterComponent,

    UsersListComponent,
    UsersListTemplateComponent,
    UserDetailsComponent,
    UserAvatarComponent,

    FollowCreateComponent,
    FollowersListComponent,
    FollowingListComponent,
    FollowingListNavBarComponent,

    NotificationsListComponent,
    NotificationSpecialInfoComponent
  ],
  exports: [
    NavBarComponent,
    NoMoreResultComponent,
    NoSearchResultsComponent,
    OrderByDropDownComponent,
    FooterComponent,

    UsersListComponent,
    UsersListTemplateComponent,
    UserDetailsComponent,
    UserAvatarComponent,

    FollowCreateComponent,
    FollowersListComponent,
    FollowingListComponent,
    FollowingListNavBarComponent,

    NotificationsListComponent,
    NotificationSpecialInfoComponent
  ],
  entryComponents: [
  
  ],

  imports: [
    MatSidenavModule,
    BrowserModule,
    BrowserAnimationsModule,
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
    SharedsRoutingModule
  ],

  providers: [
    //{ provide: AppInsightsServiceService },
    NgbActiveModal,
    { provide: LocationStrategy, useClass: HashLocationStrategy },
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    //{ provide: HTTP_INTERCEPTORS, useClass: BaseUrlInterceptor, multi: true },
  ],
  bootstrap: [AppComponent]
})
export class SharedModule { }
