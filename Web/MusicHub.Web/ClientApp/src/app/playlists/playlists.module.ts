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
import { PlaylistDeleteComponent } from './components/playlist-delete/playlist-delete.component';
import { PlaylistCreateComponent } from './components/playlist-create/playlist-create.component';
import { PlaylistEditComponent } from './components/playlist-edit/playlist-edit.component';
import { MatDialogModule } from '@angular/material';
import { PlaylistsListTemplateComponent } from './components/playlists-list-template/playlists-list-template.component';
import { PlaylistsListComponent } from './components/playlists-list/playlists-list.component';
import { PlaylistsOwnListComponent } from './components/playlists-own-list/playlists-own-list.component';
import { ChartModule } from '../charts/charts.module';
import { SharedModule } from '../shared/shared.module';
import { PlaylistSmallDetailsComponent } from './components/playlist-small-details/playlist-small-details.component';
import { PlaylistImageComponent } from './components/playlist-image/playlist-image.component';

const routes: Routes = [
  { path: '', component: PlaylistsListComponent },//, canActivate: [PermissionsGuard], data: withPermission(PermissionType.CategoryAdministrator)},
  { path: 'own', component: PlaylistsOwnListComponent, canActivate: [PermissionsGuard], data: withPermission(PermissionType.PlaylistCreator) },
];

@NgModule({
  declarations: [
    PlaylistDeleteComponent,
    PlaylistCreateComponent,
    PlaylistEditComponent,
    PlaylistsListTemplateComponent,
    PlaylistsListComponent,
    PlaylistsOwnListComponent,
    PlaylistSmallDetailsComponent,
    PlaylistImageComponent
  ],
  exports: [
    PlaylistDeleteComponent,
    PlaylistCreateComponent,
    PlaylistEditComponent,
    PlaylistsListTemplateComponent,
    PlaylistsListComponent,
    PlaylistsOwnListComponent
  ],
  entryComponents: [
    PlaylistDeleteComponent,
    PlaylistCreateComponent,
    PlaylistEditComponent
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
