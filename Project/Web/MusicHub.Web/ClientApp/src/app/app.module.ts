import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { NgbModule, NgbDropdownModule, NgbModalModule } from '@ng-bootstrap/ng-bootstrap';
import { MatSidenavModule } from '@angular/material/sidenav';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './auth/login/login.component';
import { SignupComponent } from './auth/signup/signup.component';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { JwtInterceptor } from './core/interceptors/jwt.interceptor';
import { BaseUrlInterceptor } from './core/interceptors/base-url.interceptor';
import { NavBarComponent } from './components/shared/nav-bar/nav-bar.component';
import { FooterComponent } from './components/shared/footer/footer.component';

import { ProfileChangePasswordComponent } from './components/profile/profile-change-password/profile-change-password.component';
import { ListenedSongHistoryDeleteComponent } from './components/listenedSongsHistory/listened-song-history-delete/listened-song-history-delete.component';
import { SongCreateComponent } from './components/song/song-create/song-create.component';
import { SongListComponent } from './components/song/song-list/song-list.component';
import { SongDeleteModalComponent } from './components/song/song-delete-modal/song-delete-modal.component';
import { SongEditComponent } from './components/song/song-edit/song-edit.component';
import { SongPlayComponent } from './components/song/song-play/song-play.component';
import { CommentListComponent } from './components/comment/comment-list/comment-list.component';
import { CommentInfoComponent } from './components/comment/comment-info/comment-info.component';
import { CommentCreateComponent } from './components/comment/comment-create/comment-create.component';
import { MyProfileComponent } from './components/profile/my-profile/my-profile.component';
import { ProfileEditComponent } from './components/profile/profile-edit/profile-edit.component';
import { PlaylistListComponent } from './components/playlist/playlist-list/playlist-list.component';
import { PlaylistCreateComponent } from './components/playlist/playlist-create/playlist-create.component';
import { PlaylistEditComponent } from './components/playlist/playlist-edit/playlist-edit.component';
import { PlaylistDeleteComponent } from './components/playlist/playlist-delete/playlist-delete.component';
import { SongReactionComponent } from './components/song/song-reaction/song-reaction.component';
import { CommentReactionComponent } from './components/comment/comment-reaction/comment-reaction.component';
import { PlaylistAddSongComponent } from './components/playlist/playlist-add-song/playlist-add-song.component';
import { PlayComponent } from './components/song/play/play.component';
import { CommentDeleteComponent } from './components/comment/comment-delete/comment-delete.component';
import { PlaylistPlayComponent } from './components/playlist/playlist-play/playlist-play.component';
import { PlaylistListAllComponent } from './components/playlist/playlist-list-all/playlist-list-all.component';
import { PlaylistSongDeleteComponent } from './components/playlist/playlist-song-delete/playlist-song-delete.component';
import { SongSuggestComponent } from './components/song/song-suggest/song-suggest.component';
import { UserProfileComponent } from './components/profile/user-profile/user-profile.component';
import { SongListTemplateComponent } from './components/song/song-list-template/song-list-template.component';
import { PlaylistListTemplateComponent } from './components/playlist/playlist-list-template/playlist-list-template.component';
import { FollowInfoComponent } from './components/follow/follow-info/follow-info.component';
import { FollowersListComponent } from './components/follow/followers-list/followers-list.component';
import { FollowedCreateComponent } from './components/follow/followed-create/followed-create.component';
import { FollowingListComponent } from './components/follow/following-list/following-list.component';
import { ForgottenPasswordComponent } from './auth/forgotten-password/forgotten-password.component';
import { UserAgreementComponent } from './components/legal/user-agreement/user-agreement.component';
import { PrivacyPolicyComponent } from './components/legal/privacy-policy/privacy-policy.component';
import { LayoutModule } from '@angular/cdk/layout';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { LikedSongsListComponent } from './components/song/liked-songs-list/liked-songs-list.component';
import { ListenedSongHistoryAllComponent } from './components/listenedSongsHistory/listened-song-history-all/listened-song-history-all.component';
import { LicenseCreateComponent } from './components/license/license-create/license-create.component';
import { LicenseListComponent } from './components/license/license-list/license-list.component';
import { LicenseAllListComponent } from './components/license/license-all-list/license-all-list.component';
import { LicenseDetailComponent } from './components/license/license-detail/license-detail.component';
import { LicenseDeleteComponent } from './components/license/license-delete/license-delete.component';
import { LicenseListTemplateComponent } from './components/license/license-list-template/license-list-template.component';
import { HashLocationStrategy, LocationStrategy } from '@angular/common';
import { LicenseRequestComponent } from './components/license/license-request/license-request.component';
import { CategoryDeleteModalComponent } from './components/category/category-delete-modal/category-delete-modal.component';
import { CategoryListComponent } from './components/category/category-list/category-list.component';
import { CategoryCreateComponent } from './components/category/category-create/category-create.component';
import { CategoryEditComponent } from './components/category/category-edit/category-edit.component';
import { FollowingListnavBarComponent } from './components/follow/following-listnav-bar/following-listnav-bar.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    LoginComponent,
    SignupComponent,
    NavBarComponent,
    FooterComponent,
    CategoryCreateComponent,
    CategoryListComponent,
    CategoryDeleteModalComponent,
    CategoryEditComponent,
    SongCreateComponent,
    SongListComponent,
    SongDeleteModalComponent,
    SongEditComponent,
    SongPlayComponent,
    CommentListComponent,
    CommentInfoComponent,
    CommentCreateComponent,
    MyProfileComponent,
    ProfileEditComponent,
    PlaylistListComponent,
    PlaylistCreateComponent,
    PlaylistEditComponent,
    PlaylistDeleteComponent,
    SongReactionComponent,
    CommentReactionComponent,
    PlaylistAddSongComponent,
    PlayComponent,
    CommentDeleteComponent,
    PlaylistPlayComponent,
    PlaylistListAllComponent,
    PlaylistSongDeleteComponent,
    SongSuggestComponent,
    ProfileChangePasswordComponent,
    ListenedSongHistoryAllComponent,
    ListenedSongHistoryDeleteComponent,
    UserProfileComponent,
    SongListTemplateComponent,
    PlaylistListTemplateComponent,
    FollowInfoComponent,
    FollowersListComponent,
    FollowingListComponent,
    FollowedCreateComponent,
    FollowingListComponent,
    ForgottenPasswordComponent,
    UserAgreementComponent,
    PrivacyPolicyComponent,
    LikedSongsListComponent,
    LicenseCreateComponent,
    LicenseListComponent,
    LicenseAllListComponent,
    LicenseDetailComponent,
    LicenseDeleteComponent,
    LicenseListTemplateComponent,
    LicenseRequestComponent,
    FollowingListnavBarComponent,
  ],
  entryComponents: [
    CategoryDeleteModalComponent,
    CategoryEditComponent,
    SongDeleteModalComponent,
    ProfileEditComponent,
    PlaylistCreateComponent,
    PlaylistEditComponent,
    PlaylistAddSongComponent,
    PlaylistDeleteComponent,
    CommentDeleteComponent,
    PlaylistSongDeleteComponent,
    ProfileChangePasswordComponent,
    FollowersListComponent,
    FollowingListComponent,
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
  ],
  providers: [
    //{ provide: AppInsightsServiceService},
    { provide: LocationStrategy, useClass: HashLocationStrategy },
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: BaseUrlInterceptor, multi: true },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
