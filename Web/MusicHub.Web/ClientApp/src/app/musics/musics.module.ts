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
import { MatDialogModule, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { MusicsListTemplateComponent } from './components/musics-list-template/musics-list-template.component';
import { MusicsOwnListComponent } from './components/musics-own-list/musics-own-list.component';
import { MusicCreateComponent } from './components/music-create/music-create.component';
import { MusicEditComponent } from './components/music-edit/music-edit.component';
import { MusicDeleteComponent } from './components/music-delete/music-delete.component';
import { MusicDetailsComponent } from './components/music-details/music-details.component';
import { MusicSuggestionsListComponent } from './components/music-suggestions-list/music-suggestions-list.component';
import { MusicPlayComponent } from './components/music-play/music-play.component';
import { MusicReactionComponent } from './components/music-reaction/music-reaction.component';
import { LikedMusicsListComponent } from './components/liked-musics-list/liked-musics-list.component';
import { PlayedMusicsListsComponent } from './components/played-musics-lists/played-musics-lists.component';
import { OwnMusicsAnalysisComponent } from './components/own-musics-analysis/own-musics-analysis.component';
import { MusicImageComponent } from './components/music-image/music-image.component';
import { MusicSmallDetailsComponent } from './components/music-small-details/music-small-details.component';
import { UsersModule } from '../users/users.module';
import { FollowsModule } from '../follow/follows.module';
import { CommentModule } from '../comments/comments.module';
import { SharedModule } from '../shared/shared.module';
import { ChartModule } from '../charts/charts.module';
import { MusicResolver } from '../core/resolvers/music.resolver';
import { AuthorizeGuard } from '../core/guards/auth.guard';
import { MusicAddToComponent } from './components/music-add-to/music-add-to.component';
import { AddMusicToPlaylistComponent } from './components/add-music-to-playlist/add-music-to-playlist.component';
import { NgxChartsModule } from '@swimlane/ngx-charts';
import { AddMusicToPlaylistsComponent } from './components/add-music-to-playlists/add-music-to-playlists.component';

const routes: Routes = [
  {
    path: 'play/:id',
    component: MusicPlayComponent,
    resolve:
    {
      music: MusicResolver
    },
  },
  { path: 'liked', component: LikedMusicsListComponent, canActivate: [AuthorizeGuard] },
  { path: 'history', component: PlayedMusicsListsComponent, canActivate: [AuthorizeGuard] },
  {
    path: 'own', children: [
      {
        path: '',
        component: MusicsOwnListComponent,
        canActivate: [PermissionsGuard],
        data: withPermission(PermissionType.MusicCreator)
      },
      {
        path: 'analysis',
        component: OwnMusicsAnalysisComponent,
        canActivate: [PermissionsGuard],
        data: withPermission(PermissionType.MusicCreator)
      }
    ]

  },
  {
    path: 'create',
    component: MusicCreateComponent,
    canActivate: [PermissionsGuard],
    data: withPermission(PermissionType.MusicCreator)
  },
  {
    path: 'edit/:id',
    component: MusicCreateComponent,
    resolve:
    {
      music: MusicResolver
    },
    canActivate: [AuthorizeGuard],
    data: withPermission(PermissionType.MusicCreator)
  },
];

@NgModule({
  declarations: [
    MusicsListTemplateComponent,
    MusicsOwnListComponent,
    MusicCreateComponent,
    MusicEditComponent,
    MusicDeleteComponent,
    MusicDetailsComponent,
    MusicSuggestionsListComponent,
    MusicPlayComponent,
    MusicReactionComponent,
    LikedMusicsListComponent,
    PlayedMusicsListsComponent,
    OwnMusicsAnalysisComponent,
    MusicImageComponent,
    MusicSmallDetailsComponent,
    MusicAddToComponent,
    AddMusicToPlaylistComponent,
    AddMusicToPlaylistsComponent
  ],
  exports: [
    MusicsListTemplateComponent,
    MusicsOwnListComponent,
    MusicCreateComponent,
    MusicEditComponent,
    MusicDeleteComponent,
    MusicDetailsComponent,
    MusicSuggestionsListComponent,
    MusicPlayComponent,
    MusicReactionComponent,
    LikedMusicsListComponent,
    PlayedMusicsListsComponent,
    OwnMusicsAnalysisComponent,
    MusicImageComponent,
    MusicSmallDetailsComponent,
  ],
  entryComponents: [
    MusicDeleteComponent,
    AddMusicToPlaylistComponent
  ],

  imports: [
    CommonModule,
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
    RouterModule.forChild(routes),
    SharedModule,
    CommentModule,
    ChartModule,
  ],
  providers: [
    //{ provide: AppInsightsServiceService },
    NgbActiveModal,
    { provide: LocationStrategy, useClass: HashLocationStrategy },
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    //{ provide: HTTP_INTERCEPTORS, useClass: BaseUrlInterceptor, multi: true },
    { provide: MatDialogRef, useValue: {} },
    { provide: MAT_DIALOG_DATA, useValue: {} }
  ],
  bootstrap: [AppComponent]
})
export class MusicsModule { }
