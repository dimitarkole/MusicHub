import { Component, HostListener, OnInit } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import { cwd } from 'process';
import { globalConstants } from '../../../common/global-constants';
import getPage from '../../../common/paginator';
import { BooleanSearchService } from '../../../core/resolvers/boolean-search.service';
import { AuthService } from '../../../core/services/auth.service';
import { BooleanSearchEntity } from '../../../shared/models/boolean-search';
import Category from '../../models/category';
import CategoryPagination from '../../models/categoryPagination';
import { CategoryService } from '../../services/category.service';
import { CategoryAnalysisComponent } from '../category-analysis/category-analysis.component';
import { CategoryCreateComponent } from '../category-create/category-create.component';
import { CategoryDeleteComponent } from '../category-delete/category-delete.component';
import { CategoryEditComponent } from '../category-edit/category-edit.component';

@Component({
  selector: 'app-categories-list',
  templateUrl: './categories-list.component.html',
  styleUrls: ['./categories-list.component.css']
})
export class CategoriesListComponent implements OnInit {
  page: number = globalConstants.pagination.defaultPage;
  collectionSize: number;
  categories: Category[] = [];
  categoryPagination: CategoryPagination = { currentPage: 0, numberOfPages: 1, categories: [] };
  allCategories: Category[] = [];
  search: string = "";
  isSearching: boolean = false;

  constructor(private categoryService: CategoryService,
    public matDialog: MatDialog,
    private route: ActivatedRoute,
    private searchService: BooleanSearchService,
    private authService: AuthService) {
  }
    ngOnInit(): void {
      this.feachData();
    }

  @HostListener("window:scroll", [])
  onWindowScroll() {
    //In chrome and some browser scroll is given to body tag
    let pos = (document.documentElement.scrollTop || document.body.scrollTop) + document.documentElement.offsetHeight;
    let max = document.documentElement.scrollHeight;
    // pos/max will give you the distance between scroll bottom and and bottom of screen in percentage.
    if (pos > max * globalConstants.pagination.updateProcent) {
      //Do your action here
      this.feachData();
    }
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
  public feachData(): void {
    if (this.categoryPagination.currentPage < this.categoryPagination.numberOfPages) {
      this.route.queryParams.subscribe(queryParams => {
        let query = queryParams.searchQuery;
        if (!query) {
          if (this.isSearching == true) {
            this.ResetData(query);
            this.isSearching = false;
          }

          var page = this.categoryPagination.currentPage + 1;
          this.categoryService.allByPage(page).subscribe(data => {
            this.categoryPagination.numberOfPages = data.numberOfPages;
            data.categories.forEach(category => {
              this.categoryPagination.categories.push(category);
            });
          });
        }
        else {
          this.isSearching = true;
          if (this.search != query) {
            this.ResetData(query);
          }

          var page = this.categoryPagination.currentPage + 1;
          this.searchService.search<CategoryPagination>({ query }, BooleanSearchEntity.Categories, page).subscribe(data => {
            this.categoryPagination.numberOfPages = data.numberOfPages
            data.categories.forEach(category => {
              this.categoryPagination.categories.push(category);
            });
          });
        }

        this.categoryPagination.currentPage = page;
      });
    }
  }

  private ResetData(query: any) {
    this.search = query;
    this.categoryPagination = { currentPage: 0, numberOfPages: 1, categories: [] };
  }
}
