import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material';
import { ModalComponent } from 'ngb-modal';
import { minLengthArray } from '../../../common/confirmed.validator';
import { PermissionService } from '../../../core/services/permission.service';
import { Permission } from '../../../shared/models/permission';
import { PermissionType } from '../../../shared/models/permissionType';
import { Role } from '../../../shared/models/role';
import { RoleService } from '../../services/role.service';

@Component({
  selector: 'app-role-create',
  templateUrl: './role-create.component.html',
  styleUrls: ['./role-create.component.css']
})
export class RoleCreateComponent implements OnInit {
  role: Role;
  permissions: Permission[];
  selectedPermissions: Permission[] = [];
  nameMinLength = 2;
  nameMaxLength = 30;
  roleForm: FormGroup

  constructor(
    public dialogRef: MatDialogRef<ModalComponent>,
    private roleService: RoleService,
    private permissionService: PermissionService,
    private formBuilder: FormBuilder,
  ) {
    this.permissionService.all().subscribe(p =>
      this.permissions = p);
  }

  ngOnInit() {
    this.roleForm = this.formBuilder.group({
      name: [
        null,
        [
          Validators.required,
          Validators.minLength(this.nameMinLength),
          Validators.maxLength(this.nameMaxLength)
        ]
      ],
      permissionIds: this.formBuilder.array([])
    })
  }

  // If the user clicks the cancel button a.k.a. the go back button, then\
  // just close the modal
  closeModal() {
    this.dialogRef.close();
  }

  OnSubmit() {
    let role: Role = this.roleForm.value;
    this.roleService.create(role)
      .subscribe(_ => {
        this.dialogRef.close(); //It closes successfully
      })
  }

  addPermission() {
    //element id -> select-permissions
    var e = (document.getElementById("select-permissions")) as HTMLSelectElement;
    var sel = e.selectedIndex;
    var opt = e.options[sel];
    var curValue = opt.value;
    const permissionForm = this.formBuilder.group({
      permissionId: curValue
    });

    this.permissionIds.push(permissionForm);
    var curValueNumber = +curValue;
    var permission = this.permissions.filter(p => p.id == curValueNumber)[0];
    this.selectedPermissions.push(permission);
    this.permissions = this.permissions.filter(p => p.id != curValueNumber);
  }

  removePermission(permission: Permission) {
    this.selectedPermissions = this.selectedPermissions.filter(p => p.id != permission.id);
    this.permissions.push(permission);
    this.permissions = this.permissions.sort(p => p.id);
  }

  get name(): AbstractControl {
    return this.roleForm.get('name');
  }
  
  get permissionIds() {
    return this.roleForm.get('permissionIds') as FormArray;
  }
}
