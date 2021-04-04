import { Component, OnInit } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material';
import { Router } from '@angular/router';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import { cwd } from 'process';
import { globalConstants } from '../../../common/global-constants';
import getPage from '../../../common/paginator';
import { AuthService } from '../../../core/services/auth.service';
import Category from '../../models/category';
import { CategoryService } from '../../services/category.service';
import { CategoryCreateComponent } from '../category-create/category-create.component';
import { CategoryDeleteComponent } from '../category-delete/category-delete.component';
import { CategoryEditComponent } from '../category-edit/category-edit.component';

@Component({
  selector: 'app-categories-list',
  templateUrl: './categories-list.component.html',
  styleUrls: ['./categories-list.component.css']
})
export class CategoriesListComponent {
  page: number = globalConstants.pagination.defaultPage;
  collectionSize: number;
  private itemsPerPage: number;
  categories: Category[] = [];
  allCategories: Category[] = [];

  constructor(private modalService: NgbModal,
    private categoryService: CategoryService,
    private router: Router,
    public matDialog: MatDialog,
    private authService: AuthService) {
    this.itemsPerPage = globalConstants.pagination.itemsPerPage;
    this.categoryService.all().subscribe(data => {
      this.allCategories = data;
      this.getCategoriesPerPage(this.page);
    })
  }

  openDelete(categoryId: number) {
    const dialogConfig = new MatDialogConfig();
    // The user can't close the dialog by clicking outside its body
    dialogConfig.id = "modal-component";
    dialogConfig.width = "600px";
    dialogConfig.data = categoryId;
    // https://material.angular.io/components/dialog/overview
    const modalDialog = this.matDialog.open(CategoryDeleteComponent, dialogConfig);
    modalDialog.afterClosed().subscribe(result => {
      if (result == true) window.location.reload();
    });
  }

 openCreate() {
   const dialogConfig = new MatDialogConfig();
    // The user can't close the dialog by clicking outside its body
   dialogConfig.id = "modal-component";
   dialogConfig.width = "600px";
    // https://material.angular.io/components/dialog/overview
   const modalDialog = this.matDialog.open(CategoryCreateComponent, dialogConfig);
   modalDialog.afterClosed().subscribe(result => {
     if (result == true) window.location.reload();
   });
  }

 openEdit(category: Category) {
   const dialogConfig = new MatDialogConfig();
   // The user can't close the dialog by clicking outside its body
   dialogConfig.id = "modal-component";
   dialogConfig.width = "600px";
   dialogConfig.data = category;
   // https://material.angular.io/components/dialog/overview
   const modalDialog = this.matDialog.open(CategoryEditComponent, dialogConfig);
   modalDialog.afterClosed().subscribe(result => {
     if (result == true) window.location.reload();
   });
 }

  public getCategoriesPerPage(page: number): void {
    this.categories = getPage<Category>(this.allCategories, page, this.itemsPerPage);
  }
}
