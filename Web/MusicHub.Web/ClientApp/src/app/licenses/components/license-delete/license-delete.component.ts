import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { ModalComponent } from 'ngb-modal';
import { LicenseService } from '../../services/license.service';

@Component({
  selector: 'app-license-delete',
  templateUrl: './license-delete.component.html',
  styleUrls: ['./license-delete.component.css']
})
export class LicenseDeleteComponent {
  constructor(public dialogRef: MatDialogRef<ModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private licenseService: LicenseService) {
  }

  // If the user clicks the cancel button a.k.a. the go back button, then\
  // just close the modal
  closeModal() {
    this.dialogRef.close();
  }

  deleteLicense() {
    this.licenseService.delete(this.data).subscribe(_ => {
      this.dialogRef.close();
    });
  }
}
