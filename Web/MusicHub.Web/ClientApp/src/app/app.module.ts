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
    CommentReactionComponent
  ],
  entryComponents: [
    CategoryCreateComponent,
    CategoryDeleteComponent,
    CategoryEditComponent,
    PlaylistDeleteComponent,
    PlaylistCreateComponent,
    PlaylistEditComponent,
    CommentDeleteComponent,
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
