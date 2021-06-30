import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatDialog, MatDialogConfig } from '@angular/material';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import License from '../../models/license';
import { LicenseStatus } from '../../models/LicenseStatus';
import { LicenseService } from '../../services/license.service';
import { LicenseDeleteComponent } from '../license-delete/license-delete.component';

@Component({
  selector: 'app-license-list-template',
  templateUrl: './license-list-template.component.html',
  styleUrls: ['./license-list-template.component.css']
})
export class LicenseListTemplateComponent implements OnInit {
  @Input() licenses: License[] = [];
  @Input() type: string;
  deleteForm: FormGroup;
  licenseStatus = LicenseStatus;

  constructor(public matDialog: MatDialog,
    private formBuilder: FormBuilder,
    private router: Router) { }

  ngOnInit() {
    this.deleteForm = this.formBuilder.group({
      path: null,
      fileName: null,
    });
  }

  getSatus(status: LicenseStatus) {
    return status == LicenseStatus.Approve ? "Approve"
      : status == LicenseStatus.WaitToBeView ? "Wait to be view"
        : "Reject";
  }

  openEdit(license: License) {
    this.router.navigate(['/license/edit', license.id]);
  }

  openDelete(licenseId: number) {
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
