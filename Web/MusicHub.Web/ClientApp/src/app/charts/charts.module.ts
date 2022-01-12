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
import { MusicHubLineChartComponent } from './components/music-hub-line-chart/music-hub-line-chart.component';
import { MusicHubBarChartComponent } from './components/music-hub-bar-chart/music-hub-bar-chart.component';
import { MusicHubPieChartComponent } from './components/music-hub-pie-chart/music-hub-pie-chart.component';
import { NgxChartsModule } from '@swimlane/ngx-charts';

const routes: Routes = [
];

@NgModule({
  declarations: [
    MusicHubLineChartComponent,
    MusicHubPieChartComponent,
    MusicHubBarChartComponent,
  ],
  exports: [
    MusicHubLineChartComponent,
    MusicHubPieChartComponent,
    MusicHubBarChartComponent,
  ],
  entryComponents: [
  ],

  imports: [
    BrowserModule,
    NgxChartsModule,
    MatSidenavModule,
    NgbModule,
    LayoutModule,
    MatToolbarModule,
    MatButtonModule,
    MatIconModule,
    MatListModule,
    MatDialogModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
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
export class ChartModule { }
