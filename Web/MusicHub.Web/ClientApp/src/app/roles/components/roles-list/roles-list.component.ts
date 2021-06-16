import { Component, HostListener, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { globalConstants } from '../../../common/global-constants';
import { CreateOrderMethod } from '../../../shared/models/orderMethod';
import { Role } from '../../../shared/models/role';
import { RolePagination } from '../../models/rolePagination';
import { RoleService } from '../../services/role.service';

@Component({
  selector: 'app-roles-list',
  templateUrl: './roles-list.component.html',
  styleUrls: ['./roles-list.component.css']
})
export class RolesListComponent implements OnInit {
  rolePagination: RolePagination = { currentPage: 0, numberOfPages: 1, roles: [] };
  isFilter: boolean = false;
  searchForm: FormGroup;

  constructor(private roleService: RoleService,
    private formBuilder: FormBuilder) { }

  ngOnInit() {
    this.searchForm = this.formBuilder.group({
      name: [
        null,
      ],
      orderMethod: [
        "CreatedOnDesc"
      ]
    });

    this.getRolesPerPage();
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
        this.getRolesPerPage();
      }
      else {
        this.getFilterRolesPerPage();
      }
    }
  }

  submit() {
    let role: Role = this.createFilterModel();
    this.isFilter = true;
    this.rolePagination.currentPage = 1;
    this.roleService.filter(1, role).subscribe(data => {
      this.rolePagination.numberOfPages = data.numberOfPages;
      this.rolePagination.roles = data.roles;
    });
  }

  get name(): AbstractControl {
    return this.searchForm.get('name');
  }

  get orderMethod(): AbstractControl {
    return this.searchForm.get('orderMethod');
  }

  private getRolesPerPage(): void {
    var page = this.rolePagination.currentPage + 1;
    if (page <= this.rolePagination.numberOfPages) {
      this.rolePagination.currentPage = page;
      this.roleService.all(page).subscribe(data => {
        this.rolePagination.numberOfPages = data.numberOfPages;
        data.roles.forEach(role => {
          this.rolePagination.roles.push(role);
        });
        console.log(data);
      });
    }
  }

  private getFilterRolesPerPage(): void {
    var page = this.rolePagination.currentPage + 1;
    if (page <= this.rolePagination.numberOfPages) {
      this.rolePagination.currentPage = page;
      let roles: Role = this.createFilterModel();

      this.roleService.filter(page, roles).subscribe(data => {
        this.rolePagination.numberOfPages = data.numberOfPages;
        data.roles.forEach(role => {
          this.rolePagination.roles.push(role);
        });
      });
    }
  }

  private createFilterModel() {
    let license: Role = this.searchForm.value;
    license.orderMethod = CreateOrderMethod(this.orderMethod.value);
    return license;
  }
}
