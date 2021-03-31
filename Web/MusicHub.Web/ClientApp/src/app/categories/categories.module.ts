import { NgModule } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
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

const routes: Routes = [
  { path: '', component: CategoriesListComponent },//, canActivate: [PermissionsGuard], data: withPermission(PermissionType.CategoryAdministrator)},
  { path: 'create', component: CategoryCreateComponent },//, canActivate: [PermissionsGuard], data: withPermission(PermissionType.CategoryAdministrator) },
];

@NgModule({
  declarations: [
    CategoriesListComponent,
    CategoryCreateComponent
  ],
  imports: [
    CommonModule,
		FormsModule,
    ReactiveFormsModule,
    RouterModule.forChild(routes),
  ],
 /* entryComponents: [
    CategoryEditComponent,
    CategoryCreateComponent,
    CategoryDeleteComponent,
  ],*/
  providers: [
    DatePipe
  ]
})
export class CategoriesModule { }
