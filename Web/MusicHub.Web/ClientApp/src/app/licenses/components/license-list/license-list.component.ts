import { Component, HostListener, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { globalConstants } from '../../../common/global-constants';
import { AuthService } from '../../../core/services/auth.service';
import { CreateOrderMethod } from '../../../shared/models/orderMethod';
import License from '../../models/license';
import { LicensePagination } from '../../models/licensePagination';
import { LicenseStatus } from '../../models/LicenseStatus';
import { LicenseService } from '../../services/license.service';

@Component({
  selector: 'app-license-list',
  templateUrl: './license-list.component.html',
  styleUrls: ['./license-list.component.css']
})
export class LicenseListComponent implements OnInit {
  licensePagination: LicensePagination = { currentPage: 0, numberOfPages: 1, licenses: [] };
  isFilter: boolean = false;
  defaultCategoryId: string = 'Each category';
  searchForm: FormGroup;

  constructor(private licenseService: LicenseService,
    private modalService: NgbModal,
    private router: Router,
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
    this.licenseService.filterAll(1, license).subscribe(data => {
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
      this.licenseService.all(page).subscribe(data => {
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

      this.licenseService.filterAll(page, license).subscribe(data => {
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

  getSatus(status: LicenseStatus) {
    return status == LicenseStatus.Approve ? "Approve"
      : status == LicenseStatus.WaitToBeView ? "Wait to be view"
        : "Reject";
  }
}
