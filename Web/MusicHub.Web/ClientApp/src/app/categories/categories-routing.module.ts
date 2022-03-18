import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PermissionsGuard } from '../core/guards/permissions.guard';
import { withPermission } from '../core/guards/with-permission';
import { PermissionType } from '../shared/models/permissionType';
import { CategoriesListComponent } from './components/categories-list/categories-list.component';
import { CategoryAnalysisComponent } from './components/category-analysis/category-analysis.component';

const routes: Routes = [
  {
    path: '',
    component: CategoriesListComponent,
    canActivate: [PermissionsGuard],
    data: withPermission(PermissionType.CategoryAdministrator)
  },
  {
    path: 'analysis',
    component: CategoryAnalysisComponent,
    canActivate: [PermissionsGuard],
    data: withPermission(PermissionType.CategoryAdministrator)
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CategoriesRoutingModule { }
