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
import { AlbumCreateComponent } from './components/album-create/album-create.component';
import { AlbumDeleteComponent } from './components/album-delete/album-delete.component';
import { AlbumEditComponent } from './components/album-edit/album-edit.component';
import { AlbumImageComponent } from './components/album-image/album-image.component';
import { AlbumsListComponent } from './components/albums-list/albums-list.component';
import { AlbumsOwnListComponent } from './components/albums-own-list/albums-own-list.component';
import { AlbumsListTemplateComponent } from './components/albums-list-template/albums-list-template.component';
import { AlbumPlayComponent } from './components/album-play/album-play.component';
import { MatDialogModule } from '@angular/material';
import { ChartModule } from '../charts/charts.module';
import { SharedModule } from '../shared/shared.module';
import { MusicsModule } from '../musics/musics.module';
import { CommentModule } from '../comments/comments.module';
import { AlbumSmallDetailsComponent } from './components/album-small-details/album-small-details.component';

@NgModule({
  declarations: [
    AlbumCreateComponent,
    AlbumDeleteComponent,
    AlbumEditComponent,
    AlbumImageComponent,
    AlbumsListComponent,
    AlbumsOwnListComponent,
    AlbumsListTemplateComponent,
    AlbumPlayComponent,
    AlbumSmallDetailsComponent
  ],
  imports: [
    CommonModule,
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
  ]
})
export class AlbumsModule { }
