import { DatePipe } from '@angular/common';
import { Component, HostListener, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { globalConstants } from '../../../common/global-constants';
import { BooleanSearchService } from '../../../core/resolvers/boolean-search.service';
import { AuthService } from '../../../core/services/auth.service';
import { BooleanSearchEntity } from '../../../shared/models/boolean-search';
import { UserFilter } from '../../../shared/models/filterModels';
import { CreateOrderMethod } from '../../../shared/models/orderMethod';
import { Permission } from '../../../shared/models/permission';
import User from '../../../shared/models/user';
import { UserPagination } from '../../models/userPagination';

@Component({
  selector: 'app-users-list',
  templateUrl: './users-list.component.html',
  styleUrls: ['./users-list.component.css']
})
export class UsersListComponent implements OnInit {
  userPagination: UserPagination = { currentPage: 0, numberOfPages: 1, users: [] };
  isFilter: boolean = false;
  search: string = "";
  isSearching: boolean = false;

  constructor(private router: Router,
    private route: ActivatedRoute,
		private searchService: BooleanSearchService,
    private authService: AuthService) {
  }

  ngOnInit() {
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

  private feachData(): void {
    if (this.userPagination.currentPage < this.userPagination.numberOfPages) {
      this.route.queryParams.subscribe(queryParams => {
        let query = queryParams.searchQuery;
        if (!query) {
          if (this.isSearching == true) {
            this.ResetData(query);
            this.isSearching = false;
          }

          var page = this.userPagination.currentPage + 1;
          this.authService.all(page).subscribe(data => {
            this.userPagination.numberOfPages = data.numberOfPages;
            data.users.forEach(user => {
              this.userPagination.users.push(user);
            });
          });
        }
        else {
          this.isSearching = true;
          if (this.search != query) {
            this.ResetData(query);
          }

          var page = this.userPagination.currentPage + 1;
          this.searchService.search<UserPagination>({ query }, BooleanSearchEntity.Users, page).subscribe(data => {
            this.userPagination.numberOfPages = data.numberOfPages
            data.users.forEach(user => {
              this.userPagination.users.push(user);
            });
          });
        }

        this.userPagination.currentPage = page;
      });
    }
  }

  private ResetData(query: any) {
    this.search = query;
    this.userPagination = { currentPage: 0, numberOfPages: 1, users: [] };
  }
}
