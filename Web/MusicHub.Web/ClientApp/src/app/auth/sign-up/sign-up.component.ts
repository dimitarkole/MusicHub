import { formatDate } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatDialogConfig } from '@angular/material';
import { Router } from '@angular/router';
import { ConfirmedValidator, dateValidator } from '../../common/confirmed.validator';
import { AuthService } from '../../core/services/auth.service';
import User from '../../shared/models/user';
import { SendConfirmEmailComponent } from '../send-confirm-email/send-confirm-email.component';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})
export class SignUpComponent implements OnInit {
  signUpForm: FormGroup;
  wrongCredentials: boolean = false;
  successfulCredentials: boolean = false;

  maxDate: number;
  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    public matDialog: MatDialog,
    private router: Router
  ) { }

  ngOnInit() {
    this.signUpForm = this.fb.group({
      email: [null, [Validators.required, Validators.email]],
      firstName: [null, [Validators.required]],
      lastName: [null, [Validators.required]],
      username: [null, [Validators.required]],
      password: [null, [Validators.required]],
      confirmPassword: [null, [Validators.required]],
      birthday: [null, [Validators.required]],// dateValidator()]],
      privacy: [false, [Validators.required, Validators.requiredTrue]],
    }, {
      validator: ConfirmedValidator('password', 'confirmPassword')
    });
  }

  checkPrivacy(e) {
    if (e.target.checked == true) {
      this.signUpForm.get('privacy').setValue(true);
    }
    else {
      this.signUpForm.get('privacy').setValue(false);
    }
  }

  signUp() {
    let newUser: User = this.signUpForm.value;
    /*this.authService.create(newUser)
      .subscribe(_ => {
        this.successfulCredentials = true;
        setTimeout(function () { this.successfulCredentials = false }, 2000);
        this.openConfirmEmailPopUp();
      },
        err => {
          this.wrongCredentials = true;
          setTimeout(function () { this.wrongCredentials = false }, 2000);
        });*/

    this.authService.create(newUser)
      .subscribe((res: any) => {
        this.successfulCredentials = true;
        const userId = res.userId;

        this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
          this.router.navigate(['/confirm-email', userId]);
        });
      },
        err => {
          this.wrongCredentials = true;
        });
  }

  get password(): AbstractControl {
    return this.signUpForm.get('password');
  }

  get email(): AbstractControl {
    return this.signUpForm.get('email');
  }

  get username(): AbstractControl {
    return this.signUpForm.get('username');
  }

  get firstName(): AbstractControl {
    return this.signUpForm.get('firstName');
  }

  get lastName(): AbstractControl {
    return this.signUpForm.get('lastName');
  }

  get confirmPassword(): AbstractControl {
    return this.signUpForm.get('confirmPassword');
  }

  get birthday(): AbstractControl {
    return this.signUpForm.get('birthday');
  }

  get privacy(): AbstractControl {
    return this.signUpForm.get('privacy');
  }

  openConfirmEmailPopUp() {
    const dialogConfig = new MatDialogConfig();
    // The user can't close the dialog by clicking outside its body
    dialogConfig.id = "modal-component";
    dialogConfig.width = "600px";
    // https://material.angular.io/components/dialog/overview
    const modalDialog = this.matDialog.open(SendConfirmEmailComponent, dialogConfig);
    modalDialog.afterClosed().subscribe(result => {

    });
  }
}
