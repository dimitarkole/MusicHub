import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { AuthService } from '../../../core/services/auth.service';
import User from '../../../shared/models/user';
import { ProfileService } from '../../services/profile.service';
import { MatDialogRef } from '@angular/material';
import { ModalComponent } from 'ngb-modal';

@Component({
  selector: 'app-profile-edit',
  templateUrl: './profile-edit.component.html',
  styleUrls: ['./profile-edit.component.css']
})
export class ProfileEditComponent implements OnInit {
  userId: string;
  user: User;
  userEditForm: FormGroup
  firstNameMinLength = 1;
  firstNameMaxLength = 30;
  lastNameMinLength = 1;
  lastNameMaxLength = 30;

  constructor(public dialogRef: MatDialogRef<ModalComponent>,
    private modalService: NgbModal,
    public modal: NgbActiveModal,
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private profileService: ProfileService,
    private router: Router,
    private authService: AuthService) {
  }

  ngOnInit() {
    this.userEditForm = this.formBuilder.group({
      id: [''],
      firstName: ['', [Validators.required, Validators.maxLength(30)]],
      lastName: ['', [Validators.required, Validators.maxLength(30)]],
      twoFactorEnabled: [false],
      birthday: ['']
    });

    this.profileService.MyProfile().subscribe(data => {
      this.user = data;
      this.userId = data.id;
      this.userId = this.user.id;
      this.user = data;
      this.userEditForm.patchValue({
        id: data.id,
        firstName: data.firstName,
        lastName: data.lastName,
        twoFactorEnabled: data.twoFactorEnabled,
        birthday: data.birthday
      });
    })
    //console.log(this.birthday);
  }

  OnSubmit() {
    let user: User = this.userEditForm.value;
    this.profileService.edit(user).subscribe(_ => {
      this.dialogRef.close(); //It closes successfully
    });
  }

  closeModal() {
    this.dialogRef.close();
  }

  get id(): AbstractControl {
    return this.userEditForm.get('id');
  }

  get lastName(): AbstractControl {
    return this.userEditForm.get('lastName');
  }

  get firstName(): AbstractControl {
    return this.userEditForm.get('firstName');
  }

  get birthday(): AbstractControl {
    return this.userEditForm.get('birthday');
  }

  get phone(): AbstractControl {
    return this.userEditForm.get('phone');
  }

  get twoFactorEnabled(): AbstractControl {
    return this.userEditForm.get('twoFactorEnabled');
  }
}
