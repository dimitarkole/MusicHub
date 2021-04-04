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

const routes: Routes = [
  { path: '', component: CategoriesListComponent },//, canActivate: [PermissionsGuard], data: withPermission(PermissionType.CategoryAdministrator)},
  { path: 'create', component: CategoryCreateComponent },//, canActivate: [PermissionsGuard], data: withPermission(PermissionType.CategoryAdministrator) },
];

@NgModule({
  declarations: [
    CategoriesListComponent,
    CategoryCreateComponent
  ],
  entryComponents: [
    //CategoryEditComponent,
    CategoryCreateComponent,
    //CategoryDeleteComponent,
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forChild(routes),
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
    DatePipe,
    { provide: LocationStrategy, useClass: HashLocationStrategy },
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    //{ provide: HTTP_INTERCEPTORS, useClass: BaseUrlInterceptor, multi: true },
  ],
  bootstrap: [AppComponent]
})
export class CategoriesModule { }
