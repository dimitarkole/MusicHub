import { Component, HostListener, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup } from '@angular/forms';
import { MatDialog, MatDialogConfig } from '@angular/material';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { globalConstants } from '../../../common/global-constants';
import { AuthService } from '../../../core/services/auth.service';
import { CreateOrderMethod } from '../../../shared/models/orderMethod';
import License from '../../models/license';
import { LicensePagination } from '../../models/licensePagination';
import { LicenseService } from '../../services/license.service';
import { LicenseDeleteComponent } from '../license-delete/license-delete.component';

@Component({
  selector: 'app-license-own-list',
  templateUrl: './license-own-list.component.html',
  styleUrls: ['./license-own-list.component.css']
})
export class LicenseOwnListComponent implements OnInit {
  licensePagination: LicensePagination = { currentPage: 0, numberOfPages: 1, licenses: [] };
  isFilter: boolean = false;
  searchForm: FormGroup;
  constructor(private licenseService: LicenseService,
    public matDialog: MatDialog,
    private formBuilder: FormBuilder,
    private authService: AuthService) { }

  ngOnInit() {
    this.searchForm = this.formBuilder.group({
      name: [
        null,
      ],
      orderMethod: [
        "CreatedOnDesc"
      ]
    });
    this.getLicensesPerPage();
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
        this.getLicensesPerPage();
      }
      else {
        this.getFilterLicensesPerPage();
      }
    }
  }

  submit() {
    let license: License = this.createFilterModel();
    this.isFilter = true;
    this.licensePagination.currentPage = 1;
    this.licenseService.filterOwn(1, license).subscribe(data => {
      this.licensePagination.numberOfPages = data.numberOfPages;
      this.licensePagination.licenses = data.licenses;
    });
  }

  get name(): AbstractControl {
    return this.searchForm.get('name');
  }

  get orderMethod(): AbstractControl {
    return this.searchForm.get('orderMethod');
  }

  private getLicensesPerPage(): void {
    var page = this.licensePagination.currentPage + 1;
    if (page <= this.licensePagination.numberOfPages) {
      this.licensePagination.currentPage = page;
      this.licenseService.own(page).subscribe(data => {
        this.licensePagination.numberOfPages = data.numberOfPages;
        data.licenses.forEach(license => {
          this.licensePagination.licenses.push(license);
        });
        console.log(data);
      });
    }
  }

  private getFilterLicensesPerPage(): void {
    var page = this.licensePagination.currentPage + 1;
    if (page <= this.licensePagination.numberOfPages) {
      this.licensePagination.currentPage = page;
      let license: License = this.createFilterModel();

      this.licenseService.filterOwn(page, license).subscribe(data => {
        this.licensePagination.numberOfPages = data.numberOfPages;
        data.licenses.forEach(license => {
          this.licensePagination.licenses.push(license);
        });
      });
    }
  }

  private createFilterModel() {
    let license: License = this.searchForm.value;
    license.orderMethod = CreateOrderMethod(this.orderMethod.value);
    return license;
  }

  openDelete(licenseId: string) {
    const dialogConfig = new MatDialogConfig();
    // The user can't close the dialog by clicking outside its body
    dialogConfig.id = "modal-component";
    dialogConfig.width = "600px";
    dialogConfig.data = licenseId;
    // https://material.angular.io/components/dialog/overview
    const modalDialog = this.matDialog.open(LicenseDeleteComponent, dialogConfig);
    modalDialog.afterClosed().subscribe(result => {
      if (result == true) window.location.reload();
    });
  }
}
