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
import { PlayLaterMusicCreateComponent } from './components/play-later-music-create/play-later-music-create.component';
import { PlayLaterMusicsListComponent } from './components/play-later-musics-list/play-later-musics-list.component';
import { PlayLaterMusicDeleteComponent } from './components/play-later-music-delete/play-later-music-delete.component';
import { PlayLaterMusicPlayComponent } from './components/play-later-music-play/play-later-music-play.component';
import { MatDialogModule } from '@angular/material';
import { ChartModule } from '../charts/charts.module';
import { SharedModule } from '../shared/shared.module';
import { MusicsModule } from '../musics/musics.module';
import { CommentModule } from '../comments/comments.module';



@NgModule({
  declarations: [
    PlayLaterMusicCreateComponent,
    PlayLaterMusicsListComponent,
    PlayLaterMusicDeleteComponent,
    PlayLaterMusicPlayComponent
  ],
  entryComponents: [
    PlayLaterMusicDeleteComponent,
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
    ChartModule,
    SharedModule,

    MusicsModule,
    CommentModule,
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
export class PlayLaterMusicsModule { }
