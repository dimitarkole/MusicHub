import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { LicenseService } from '../../../core/services/license.service';
import License from '../../shared/models/license';
import { LicenseStatus } from '../../shared/models/LicenseStatus';
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

  constructor(private modalService: NgbModal,
    private licenseService: LicenseService,
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

  openDelete(songId: string) {
    let modal = this.modalService.open(LicenseDeleteComponent);
    modal.result.then(value => {
      this.licenseService.delete(songId).subscribe(_ => {
        this.router.navigate(['/license/own']);
      })
    }).catch(err => {
      console.log(err);
    })
  }
}
