import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { ModalComponent } from 'ngb-modal';
import { PermissionService } from '../../../core/services/permission.service';
import { Role } from '../../../shared/models/role';
import { RoleService } from '../../services/role.service';

@Component({
  selector: 'app-roles-details',
  templateUrl: './roles-details.component.html',
  styleUrls: ['./roles-details.component.css']
})
export class RolesDetailsComponent implements OnInit {
  role: Role;
  constructor(
    public dialogRef: MatDialogRef<ModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private roleService: RoleService,
    private permissionService: PermissionService,
    private formBuilder: FormBuilder,
  ) {
    this.roleService.getById(this.data.id).subscribe(r => {

      this.role = r;
      console.log(r);
    });
  }

  ngOnInit() {
  }

}
