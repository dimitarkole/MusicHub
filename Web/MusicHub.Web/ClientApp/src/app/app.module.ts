import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { NgbModule, NgbDropdownModule, NgbModalModule, NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { MatSidenavModule } from '@angular/material/sidenav';
//import { ChartsModule } from 'ng2-charts';
//import { ClipboardModule, ClipboardService } from 'ngx-clipboard';
//import { EffectsModule } from '@ngrx/effects';
//import { StoreModule } from '@ngrx/store';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { JwtInterceptor } from './core/interceptors/jwt.interceptor';
import { BaseUrlInterceptor } from './core/interceptors/base-url.interceptor';
//import { StoreDevtoolsModule } from '@ngrx/store-devtools';

import { LayoutModule } from '@angular/cdk/layout';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { HashLocationStrategy, LocationStrategy } from '@angular/common';
import { SignUpComponent } from './auth/sign-up/sign-up.component';
import { SignInComponent } from './auth/sign-in/sign-in.component';
import { NavBarComponent } from './shared/nav-bar/nav-bar.component';
import { FooterComponent } from './shared/footer/footer.component';
import { CategoryCreateComponent } from './categories/components/category-create/category-create.component';
import { CategoryDeleteComponent } from './categories/components/category-delete/category-delete.component';
import { CategoryEditComponent } from './categories/components/category-edit/category-edit.component';
import { CategoriesListComponent } from './categories/components/categories-list/categories-list.component';
import { MatDialogModule } from '@angular/material';
import { PlaylistDeleteComponent } from './playlists/components/playlist-delete/playlist-delete.component';
import { PlaylistCreateComponent } from './playlists/components/playlist-create/playlist-create.component';
import { PlaylistEditComponent } from './playlists/components/playlist-edit/playlist-edit.component';
import { PlaylistsOwnListComponent } from './playlists/components/playlists-own-list/playlists-own-list.component';
import { PlaylistsListComponent } from './playlists/components/playlists-list/playlists-list.component';
import { PlaylistsListTemplateComponent } from './playlists/components/playlists-list-template/playlists-list-template.component';
import { MusicsListTemplateComponent } from './musics/components/musics-list-template/musics-list-template.component';
import { MusicsOwnListComponent } from './musics/components/musics-own-list/musics-own-list.component';
import { MusicCreateComponent } from './musics/components/music-create/music-create.component';
import { MusicEditComponent } from './musics/components/music-edit/music-edit.component';
import { MusicDeleteComponent } from './musics/components/music-delete/music-delete.component';
import { MusicSuggestionsListComponent } from './musics/components/music-suggestions-list/music-suggestions-list.component';
import { MusicDetailsComponent } from './musics/components/music-details/music-details.component';
import { MusicPlayComponent } from './musics/components/music-play/music-play.component';
import { CommentReactionComponent } from './comments/components/comment-reaction/comment-reaction.component';
import { CommentDetailsComponent } from './comments/components/comment-details/comment-details.component';
import { CommentDeleteComponent } from './comments/components/comment-delete/comment-delete.component';
import { CommentListComponent } from './comments/components/comment-list/comment-list.component';
import { CommentCreateComponent } from './comments/components/comment-create/comment-create.component';
import { MusicReactionComponent } from './musics/components/music-reaction/music-reaction.component';
import { LicenseCreateComponent } from './licenses/components/license-create/license-create.component';
import { LicenseListTemplateComponent } from './licenses/components/license-list-template/license-list-template.component';
import { LicenseListComponent } from './licenses/components/license-list/license-list.component';
import { LicenseOwnListComponent } from './licenses/components/license-own-list/license-own-list.component';
import { LicenseEditComponent } from './licenses/components/license-edit/license-edit.component';
import { LicenseDeleteComponent } from './licenses/components/license-delete/license-delete.component';
import { LicenseRequestComponent } from './licenses/components/license-request/license-request.component';
import { ProfileOwnComponent } from './profiles/components/profile-own/profile-own.component';
import { ProfileChangePasswordComponent } from './profiles/components/profile-change-password/profile-change-password.component';
import { ProfileEditComponent } from './profiles/components/profile-edit/profile-edit.component';
import { ProfileUserComponent } from './profiles/components/profile-user/profile-user.component';
import { FollowCreateComponent } from './follow/components/follow-create/follow-create.component';
import { FollowersListComponent } from './follow/components/followers-list/followers-list.component';
import { FollowingListComponent } from './follow/components/following-list/following-list.component';
import { RolesListComponent } from './roles/components/roles-list/roles-list.component';
import { RoleCreateComponent } from './roles/components/role-create/role-create.component';
import { RolesDetailsComponent } from './roles/components/roles-details/roles-details.component';
import { RoleDeleteComponent } from './roles/components/role-delete/role-delete.component';
import { RoleEditComponent } from './roles/components/role-edit/role-edit.component';
import { UsersListComponent } from './users/components/users-list/users-list.component';
import { UsersListTemplateComponent } from './users/components/users-list-template/users-list-template.component';
import { UserDetailsComponent } from './users/components/user-details/user-details.component';
import { NoSearchResultsComponent } from './shared/no-search-results/no-search-results.component';
import { NoMoreResultComponent } from './shared/no-more-result/no-more-result.component';
import { SendConfirmEmailComponent } from './auth/send-confirm-email/send-confirm-email.component';
import { ConfirmEmailComponent } from './auth/confirm-email/confirm-email.component';
import { ResendEmailConfirmComponent } from './auth/resend-email-confirm/resend-email-confirm.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    NavBarComponent,
    FooterComponent,
    SignUpComponent,
    SignInComponent,
    CategoryCreateComponent,
    CategoriesListComponent,
    CategoryDeleteComponent,
    CategoryEditComponent,
    PlaylistsListTemplateComponent,
    PlaylistsListComponent,
    PlaylistsOwnListComponent,
    PlaylistDeleteComponent,
    PlaylistCreateComponent,
    PlaylistEditComponent,
    MusicsListTemplateComponent,
    MusicsOwnListComponent,
    MusicCreateComponent,
    MusicEditComponent,
    MusicDeleteComponent,
    MusicDetailsComponent,
    MusicSuggestionsListComponent,
    MusicReactionComponent,
    MusicPlayComponent,
    CommentCreateComponent,
    CommentListComponent,
    CommentDeleteComponent,
    CommentDetailsComponent,
    CommentReactionComponent,
    LicenseCreateComponent,
    LicenseListTemplateComponent,
    LicenseListComponent,
    LicenseOwnListComponent,
    LicenseEditComponent,
    LicenseDeleteComponent,
    LicenseDeleteComponent,
    LicenseRequestComponent,
    ProfileOwnComponent,
    ProfileChangePasswordComponent,
    ProfileEditComponent,
    ProfileUserComponent,
    FollowCreateComponent,
    FollowersListComponent,
    FollowingListComponent,
    RolesListComponent,
    RolesDetailsComponent,
    RoleCreateComponent,
    RoleDeleteComponent,
    RoleEditComponent,
    UsersListComponent,
    UsersListTemplateComponent,
    UserDetailsComponent,
    NoSearchResultsComponent,
    NoMoreResultComponent,
    SendConfirmEmailComponent,
    ConfirmEmailComponent,
    ResendEmailConfirmComponent,
  ],
  entryComponents: [
    CategoryCreateComponent,
    CategoryDeleteComponent,
    CategoryEditComponent,
    PlaylistDeleteComponent,
    PlaylistCreateComponent,
    PlaylistEditComponent,
    CommentDeleteComponent,
    LicenseDeleteComponent,
    ProfileEditComponent,
    ProfileChangePasswordComponent,
    FollowersListComponent,
    FollowingListComponent,
    RoleCreateComponent,
    RolesDetailsComponent,
    RoleDeleteComponent,
    RoleEditComponent,
    SendConfirmEmailComponent,
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
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
 /*   ChartsModule,
    ClipboardModule,
    /*StoreModule.forRoot({}, {
      runtimeChecks: {
        strictStateImmutability: true,
        strictActionImmutability: true
      }
    }),
    EffectsModule.forRoot([]),
    StoreDevtoolsModule.instrument({
      maxAge: 25,
      logOnly: environment.production,
    }),*/
    // SharedModule,
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
export class AppModule { }
