import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
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
    private authService: AuthService) {
    this.itemsPerPage = globalConstants.pagination.itemsPerPage;
    this.categoryService.all().subscribe(data => {
      this.allCategories = data;
      this.getCategoriesPerPage(this.page);
    })
  }

  openDelete(categoryId: number) {
    let modal = this.modalService.open(CategoryDeleteComponent);
    modal.result.then(value => {
      debugger;
      this.categoryService.delete(categoryId).toPromise()
        .then(_ => {
          this.router.navigate(['/category/all']);
        })
    }).catch(err => {
      console.log(err);
    })
  }

 openCreate() {
    let modal = this.modalService.open(CategoryCreateComponent);

    modal.result.then(_ => {
        this.router.navigate(['/category/all']);
      }).catch(err => {
      console.log(err);
    })
  }

 openEdit(category: Category) {
    let modal = this.modalService.open(CategoryEditComponent);

    modal.componentInstance.category = category;
    modal.result.then(_ => {
        this.router.navigate(['/category/all']);
      }).catch(err => {
      console.log(err);
    })
  }

  public getCategoriesPerPage(page: number): void {
    this.categories = getPage<Category>(this.allCategories, page, this.itemsPerPage);
  }
}
