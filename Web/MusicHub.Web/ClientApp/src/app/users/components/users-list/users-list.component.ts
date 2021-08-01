import { DatePipe } from '@angular/common';
import { Component, HostListener, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { globalConstants } from '../../../common/global-constants';
import { AuthService } from '../../../core/services/auth.service';
import { UserFilter } from '../../../shared/models/filterModels';
import { CreateOrderMethod } from '../../../shared/models/orderMethod';
import User from '../../../shared/models/user';
import { UserPagination } from '../../models/userPagination';

@Component({
  selector: 'app-users-list',
  templateUrl: './users-list.component.html',
  styleUrls: ['./users-list.component.css']
})
export class UsersListComponent implements OnInit {
  userPagination: UserPagination = { currentPage: 0, numberOfPages: 1, users: [] };
  searchForm: FormGroup;
  isFilter: boolean = false;

  constructor(private router: Router,
    private formBuilder: FormBuilder,
    private authService: AuthService) {
  }

  ngOnInit() {
    this.searchForm = this.formBuilder.group({
      userName: [
        null,
      ],
      firstName: [
        null,
      ],
      lastName: [
        null,
      ],
      email: [
        null,
      ],
      orderMethod: [
        "UserNameAsc"
      ]
    });
    this.getUsersPerPage();
  }

  @HostListener("window:scroll", [])
  onWindowScroll() {
    //In chrome and some browser scroll is given to body tag
    let pos = (document.documentElement.scrollTop || document.body.scrollTop) + document.documentElement.offsetHeight;
    let max = document.documentElement.scrollHeight;
    // pos/max will give you the distance between scroll bottom and and bottom of screen in percentage.
    if (pos > max * globalConstants.pagination.updateProcent) {
      //Do your action here
      if (this.isFilter == false) {
        this.getUsersPerPage();
      }
      else {
        this.getFilterUsersPerPage();
      }
    }
  }

  get userName(): AbstractControl {
    return this.searchForm.get('userName');
  }

  get firstName(): AbstractControl {
    return this.searchForm.get('firstName');
  }

  get lastName(): AbstractControl {
    return this.searchForm.get('lastName');
  }

  get email(): AbstractControl {
    return this.searchForm.get('email');
  }

  get orderMethod(): AbstractControl {
    return this.searchForm.get('orderMethod');
  }

  submit() {
    var page = 1;
    this.userPagination.users = [];
    this.userPagination.currentPage = page;
    let user: UserFilter = this.createFilterModel();
    this.authService.advancedFilter(page, user).subscribe(data => {
      this.userPagination.numberOfPages = data.numberOfPages;
      data.users.forEach(user => {
        this.userPagination.users.push(user);
      });
    });
  }

  private getUsersPerPage(): void {
    var page = this.userPagination.currentPage + 1;
    if (page <= this.userPagination.numberOfPages) {
      this.userPagination.currentPage = page;
      this.authService.all(page).subscribe(data => {
        this.userPagination.numberOfPages = data.numberOfPages;
        data.users.forEach(user => {
          this.userPagination.users.push(user);
        });
      });
    }
  }

  private getFilterUsersPerPage(): void {
    var page = this.userPagination.currentPage + 1;
    if (page <= this.userPagination.numberOfPages) {
      this.userPagination.currentPage = page;
      let user: UserFilter = this.createFilterModel();
      this.authService.advancedFilter(page, user).subscribe(data => {
        this.userPagination.numberOfPages = data.numberOfPages;
        data.users.forEach(user => {
          this.userPagination.users.push(user);
        });
      });
    }
  }

  private createFilterModel() {
    let user: UserFilter = this.searchForm.value;
    user.orderMethod = CreateOrderMethod(this.orderMethod.value);
    return user;
  }
}
