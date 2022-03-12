import { Component, Input, OnInit } from '@angular/core';
import Category from 'src/app/components/shared/models/category';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import { globalConstants } from 'src/app/common/global-constants';
import { CategoryService } from 'src/app/core/services/category.service';
import getPage from 'src/app/common/paginator';
import { CategoryDeleteModalComponent } from '../../category/category-delete-modal/category-delete-modal.component';
import { CategoryEditComponent } from '../../category/category-edit/category-edit.component';
import { Router } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';

@Component({
  selector: './category-list.component.html',
  templateUrl: './category-list.component.html'
})
export class CategoryListComponent {
  @Input() categoty: Category

  page: number = globalConstants.pagination.defaultPage;
  collectionSize: number;
  private itemsPerPage: number;
  categories: Category[] = [];
  allCategories: Category[] = [];

  constructor(private modalService: NgbModal,
    private categoryService: CategoryService,
    private router: Router,
    private authService: AuthService) {
    if ((authService.isAuth == false) || (authService.isAdmin == false)) {
      this.router.navigate(['']);
    }
  
    this.itemsPerPage = globalConstants.pagination.itemsPerPage;
    this.categoryService.all().subscribe(data => {
      this.allCategories = data;
      this.getCategoriesPerPage(this.page);
    })
  }

  openDelete(categoryId: number) {
    let modal = this.modalService.open(CategoryDeleteModalComponent);
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
