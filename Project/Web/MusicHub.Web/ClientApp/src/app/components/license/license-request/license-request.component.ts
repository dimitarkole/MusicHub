import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';
import { LicenseService } from '../../../core/services/license.service';
import License from '../../shared/models/license';
import { LicenseStatus } from '../../shared/models/LicenseStatus';

@Component({
  selector: 'app-license-request',
  templateUrl: './license-request.component.html',
  styleUrls: ['./license-request.component.css']
})
export class LicenseRequestComponent implements OnInit {
  isAuth: boolean = false;
  licenseId: string;
  license: License;
  requestForm: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private licenseService: LicenseService,
    private authService: AuthService) {
    if (authService.isAuth == false) {
      this.router.navigate(['']);
    }
  }

  ngOnInit() {
    this.license = this.route.snapshot.data.license as License;
    this.licenseId = this.license.id;
    this.requestForm = this.formBuilder.group({
      id: this.license.id,
      status: [
        this.license.status,
        [
          Validators.required,
          Validators.pattern('^((?!default).)*$'),
        ]
      ],
    });
  }

  approve() {
    this.requestForm.get('status').setValue(LicenseStatus.Approve);
    console.log("Approve");
    this.sendResult();
  }

  reject() {
    this.requestForm.get('status').setValue(LicenseStatus.Reject);
    console.log("reject");
    this.sendResult();
  }

  getSatus(status: LicenseStatus) {
    return status == LicenseStatus.Approve ? "Approve"
      : status == LicenseStatus.WaitToBeView ? "Wait to be view"
        : "Reject";
  }

  private sendResult() {
    let status: LicenseStatus = this.requestForm.value;
    this.licenseService.changeStatus(status, this.licenseId)
      .subscribe(_ => {
        this.router.navigate(['license/all']);
      });
  }
}
