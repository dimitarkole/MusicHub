import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { ModalComponent } from 'ngb-modal';
import { RoleService } from '../../services/role.service';

@Component({
  selector: 'app-role-delete',
  templateUrl: './role-delete.component.html',
  styleUrls: ['./role-delete.component.css']
})
export class RoleDeleteComponent {
  constructor(public dialogRef: MatDialogRef<ModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private roleService: RoleService) {
  }

  // If the user clicks the cancel button a.k.a. the go back button, then\
  // just close the modal
  closeModal() {
    this.dialogRef.close();
  }

  deleteRole() {
    this.roleService.delete(this.data).subscribe(_ => {
      this.dialogRef.close();
    });
  }
}
