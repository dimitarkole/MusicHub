import { NgModule } from '@angular/core';
import { CommonModule, DatePipe, HashLocationStrategy, LocationStrategy } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CategoriesListComponent } from './components/categories-list/categories-list.component';
import { AuthorizeGuard } from '../core/guards/auth.guard';
import { CategoryCreateComponent } from './components/category-create/category-create.component';
import { PermissionType } from '../shared/models/permissionType';
import { PermissionsGuard } from '../core/guards/permissions.guard';
import { withPermission } from '../core/guards/with-permission';
import { CategoryDeleteComponent } from './components/category-delete/category-delete.component';
import { CategoryEditComponent } from './components/category-edit/category-edit.component';
import { MatSidenavModule } from '@angular/material/sidenav';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutingModule } from '../app-routing.module';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { LayoutModule } from '@angular/cdk/layout';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { JwtInterceptor } from '../core/interceptors/jwt.interceptor';
import { BaseUrlInterceptor } from '../core/interceptors/base-url.interceptor';
import { AppComponent } from '../app.component';
import { CategoryAnalysisComponent } from './components/category-analysis/category-analysis.component';
import { FilterCategoryComponent } from './components/filter-category/filter-category.component';
import { MatDialogModule } from '@angular/material';
import { NgxChartsModule } from '@swimlane/ngx-charts';
import { ChartModule } from '../charts/charts.module';
import { SharedModule } from '../shared/shared.module';
import { CategoriesRoutingModule } from './categories-routing.module';

const routes: Routes = [
  { path: '', component: CategoriesListComponent },//, canActivate: [PermissionsGuard], data: withPermission(PermissionType.CategoryAdministrator)},
  { path: 'create', component: CategoryCreateComponent },//, canActivate: [PermissionsGuard], data: withPermission(PermissionType.CategoryAdministrator) },
];

@NgModule({
  declarations: [
    CategoriesListComponent,
    CategoryCreateComponent,
    CategoryEditComponent,
    CategoryDeleteComponent,
    CategoryAnalysisComponent,
    FilterCategoryComponent
  ],
  entryComponents: [
    CategoryCreateComponent,
    CategoryDeleteComponent,
    CategoryEditComponent,
    CategoryAnalysisComponent,
  ],

  exports: [
    CategoriesListComponent,
    CategoryCreateComponent,
    CategoryEditComponent,
    CategoryDeleteComponent,
    CategoryAnalysisComponent,
    FilterCategoryComponent
  ],
  imports: [
    BrowserModule,
    NgxChartsModule,
    BrowserAnimationsModule,
    MatSidenavModule,
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
    ChartModule,  
    CategoriesRoutingModule,
    RouterModule.forChild(routes),
  ],
  providers: [
    DatePipe,
    { provide: LocationStrategy, useClass: HashLocationStrategy },
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    //{ provide: HTTP_INTERCEPTORS, useClass: BaseUrlInterceptor, multi: true },
  ],
  bootstrap: [AppComponent]
})
export class CategoriesModule { }
