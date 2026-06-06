import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup } from '@angular/forms';
import { MatDialog, MatDialogConfig } from '@angular/material';

import { UserIpRuleService } from '../../services/user-ip-rule.service';

import { UserIpRuleFilter } from '../../models/userIpRuleFilter';
import { UserIpRulePageModel } from '../../models/userIpRulePageModel';

import { IpRuleType } from '../../models/ipRuleType';
import { UserIpRuleCreateComponent } from '../user-ip-rule-create/user-ip-rule-create.component';

@Component({
  selector: 'app-user-ip-rule-list',
  templateUrl: './user-ip-rule-list.component.html',
  styleUrls: ['./user-ip-rule-list.component.css']
})
export class UserIpRuleListComponent implements OnInit {

  pagination: UserIpRulePageModel = {
    currentPage: 0,
    numberOfPages: 1,
    userIpRules: []
  };

  searchForm: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    public matDialog: MatDialog,
    private userIpRuleService: UserIpRuleService) {
  }

  ngOnInit(): void {
    this.preparedPage();
  }

  private preparedPage() {
    this.searchForm = this.formBuilder.group({
      pattern: [
        null
      ],
      ruleType: [
        IpRuleType.All
      ]
    });

    this.getRulesPerPage();
  }

  get pattern(): AbstractControl {
    return this.searchForm.get('pattern');
  }

  get ruleType(): AbstractControl {
    return this.searchForm.get('ruleType');
  }

  search() {
    this.pagination.currentPage = 0;
    this.pagination.numberOfPages = 1;
    this.pagination.userIpRules = [];

    this.getRulesPerPage();
  }

  private getRulesPerPage(): void {
    const page = this.pagination.currentPage + 1;
    if (page <= this.pagination.numberOfPages) {
      this.pagination.currentPage = page;
      const filter = this.createFilterModel();
      this.userIpRuleService
        .Filter(filter, page, 10)
        .subscribe(data => {
          this.pagination.numberOfPages = data.numberOfPages;
          data.userIpRules.forEach(rule => {
            this.pagination.userIpRules.push(rule);
          });
        });
    }
  }

  openCreate() {
    const dialogConfig = new MatDialogConfig();
    // The user can't close the dialog by clicking outside its body
    dialogConfig.id = "modal-component";
    dialogConfig.width = "800px";
    // https://material.angular.io/components/dialog/overview
    const modalDialog = this.matDialog.open(UserIpRuleCreateComponent, dialogConfig);
    modalDialog.afterClosed().subscribe(result => {
      if (result == true) window.location.reload();
    });
  }

  private createFilterModel(): UserIpRuleFilter {
    let filter: UserIpRuleFilter = this.searchForm.value;
    return filter;
  }
}
