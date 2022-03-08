import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { NgbModule, NgbDropdownModule, NgbModalModule, NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { MatSidenavModule } from '@angular/material/sidenav';
import { NgxChartsModule } from '@swimlane/ngx-charts';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { JwtInterceptor } from './core/interceptors/jwt.interceptor';
import { BaseUrlInterceptor } from './core/interceptors/base-url.interceptor';

import { LayoutModule } from '@angular/cdk/layout';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { CommonModule, HashLocationStrategy, LocationStrategy } from '@angular/common';
import { environment } from '../environments/environment';
import { SimpleModule } from '../simple/simple.module';

import { SimpleChartComponent } from './simple-chart/simple-chart.component';
import { MatDialogModule } from '@angular/material';
import { CategoriesModule } from './categories/categories.module';
import { ChartModule } from './charts/charts.module';
import { PlaylistsModule } from './playlists/playlists.module';
import { NotificationsModule } from './notifications/notifications.module';
import { FollowsModule } from './follow/follows.module';
import { UsersModule } from './users/users.module';
import { MusicsModule } from './musics/musics.module';
import { LicensesModule } from './licenses/licenses.module';
import { CommentModule } from './comments/comments.module';
import { RolesModule } from './roles/roles.module';
import { ProfileModule } from './profiles/profile.module';
import { AuthsModule } from './auth/auths.module';
import { SharedModule } from './shared/shared.module';
//import { StoreModule } from '@ngrx/store';
import { EffectsModule } from '@ngrx/effects';
import { StoreModule } from '@ngrx/store';
/*
import { StoreModule } from '@ngrx/store';
import { ClipboardModule, ClipboardService } from 'ngx-clipboard';
import { EffectsModule } from '@ngrx/effects';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';*/
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import { AlbumsModule } from './albums/albums.module';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    SimpleChartComponent,
  ],
  entryComponents: [
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
    FormsModule,

    // FollowsModule,
    // UsersModule,
    // NotificationsModule,

    SharedModule,

    CategoriesModule,
    ChartModule,
    PlaylistsModule,
    MusicsModule,
    LicensesModule,
    CommentModule,
    RolesModule,
    ProfileModule,
    AuthsModule,
    AlbumsModule,

    StoreModule.forRoot({}, {
      runtimeChecks: {
        strictStateImmutability: true,
        strictActionImmutability: true
      }
    }),
    EffectsModule.forRoot([]),
    StoreDevtoolsModule.instrument({
      maxAge: 25,
      logOnly: environment.production,
    }),
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
