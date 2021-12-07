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

const routes: Routes = [
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
    MatDialogModule
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
export class PlaylistsModule { }
