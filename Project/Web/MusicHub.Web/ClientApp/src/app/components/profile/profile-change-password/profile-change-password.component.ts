import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, AbstractControl } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { AuthService } from '../../../core/services/auth.service';
import User from '../../shared/models/user';
import { ProfileService } from '../../../core/services/profile.service';

@Component({
  selector: 'app-profile-change-password',
  templateUrl: './profile-change-password.component.html',
  styleUrls: ['./profile-change-password.component.css']
})
export class ProfileChangePasswordComponent {//implements OnInit {
  userId: string;
  wrongCredentials: boolean = false;
  user: User;
  changePassworForm: FormGroup
  public message: string;
  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    public modal: NgbActiveModal,
    private authService: AuthService,
    private profileService: ProfileService) {
    if (authService.isAuth == false) {
      this.router.navigate(['']);
    }
   

  }
  
  ngOnInit() {
    this.profileService.MyProfile().subscribe(data => {
      this.user = data;
      this.userId = data.id;

      this.changePassworForm = this.formBuilder.group({
        id: this.user.id,
        currentPassword: [
          null,
          [
            Validators.required
          ]
        ],
        newPassword: [
          null,
          [
            Validators.required
          ]
        ],
      })
    })
  }

  OnSubmit() {
    let user: User = this.changePassworForm.value;

    this.profileService.changePassword(user)
      .subscribe(_ => {
        this.modal.close(); //It closes successfully
      },
      err => {
        this.wrongCredentials = true;
        setTimeout(function () { this.wrongCredentials = false }, 2000);
      });
  }

  get currentPassword(): AbstractControl {
    return this.changePassworForm.get('currentPassword');
  }

  get newPassword(): AbstractControl {
    return this.changePassworForm.get('newPassword');
  }
}
