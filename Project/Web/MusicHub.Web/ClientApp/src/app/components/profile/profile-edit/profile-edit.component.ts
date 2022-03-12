import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { Observable } from 'rxjs';
import { FormGroup, FormBuilder, Validators, AbstractControl } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { HttpEventType } from '@angular/common/http';
import User from '../../shared/models/user';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { ProfileService } from '../../../core/services/profile.service';
import { AuthService } from '../../../core/services/auth.service';


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

  constructor(
    public modal: NgbActiveModal,
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private profileService: ProfileService,
    private router: Router,
    private authService: AuthService) {
    if (authService.isAuth == false) {
      this.router.navigate(['']);
    }
  }

  ngOnInit() {
    this.profileService.MyProfile().subscribe(data => {
      this.user = data;
      this.userId = data.id;

      this.userId = this.user.id;
      this.userEditForm = this.formBuilder.group({
        id: this.user.id,
        firstName: [
          this.user.firstName,
          [Validators.required, Validators.minLength(1), Validators.maxLength(30)],
        ],
        lastName: [
          this.user.lastName,
          [Validators.required, Validators.minLength(1), Validators.maxLength(30)]
        ],
        birthday: [
          this.user.birthday,
          [Validators.required]
        ],
      });
    })
    //console.log(this.birthday);
  }

  OnSubmit() {
    let user: User = this.userEditForm.value;
    this.profileService.edit(user)
      .subscribe(_ => {
        this.modal.close(); //It closes successfully
      })
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
}
