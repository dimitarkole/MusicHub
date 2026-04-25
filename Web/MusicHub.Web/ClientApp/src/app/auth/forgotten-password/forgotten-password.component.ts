import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ConfirmedValidator } from '../../common/confirmed.validator';
import { AuthService } from '../../core/services/auth.service';
import { EmailService } from '../../core/services/email.service';
import { VerificationCodeService } from '../../core/services/verification-code.service';
import { ProfileService } from '../../profiles/services/profile.service';
import User from '../../shared/models/user';
import VerificationCode from '../../shared/models/verificationCode';
import { CreateVerificationType, VerificationType } from '../../shared/models/verificationType';

@Component({
  selector: 'app-forgotten-password',
  templateUrl: './forgotten-password.component.html',
  styleUrls: ['./forgotten-password.component.css']
})
export class ForgottenPasswordComponent implements OnInit {
  emailForm: FormGroup;
  changePasswordForm: FormGroup;
  wrongEmail: boolean = false;
  wrongCreateCode: boolean = false;
  wrongSendEmail: boolean = false;
  wrongChangePassword: boolean = false;
  successfulCredentials: boolean = false;
  wrongCode: boolean = false;
  isSetEmail: boolean = false;
  user: User;

  constructor(
    private verificationCodeService: VerificationCodeService,
    private emailService: EmailService,
    private profileService: ProfileService,
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) { }

  ngOnInit() {
    this.emailForm = this.fb.group({
      userId: [null],
      type: VerificationType.ChangePassword,
      email: [null, [Validators.required, Validators.email]],
    })

    this.changePasswordForm = this.fb.group({
      code: [null, [Validators.required]],
      password: [null, [Validators.required]],
      confirmPassword: [null, [Validators.required]],
    },
    {
      validator: ConfirmedValidator('password', 'confirmPassword')
    });
  }

  setUser() {
    let user: User = this.emailForm.value;

    this.authService.getByEmail(user)
      .subscribe(getUser => {
        if ((getUser == null)) {
          this.wrongEmail = true;
          setTimeout(function () { this.wrongCredentials = false }, 2000);
        }
        else {
          this.user = getUser;
          this.sendEmail();
          let user1: User = this.changePasswordForm.value;
          console.log(user1)
        }
      },
        err => {
          this.wrongEmail = true;
        setTimeout(function () { this.wrongCredentials = false }, 2000);
      });
  }

  changePassword() {
    let verificationCodeInfo: VerificationCode = this.changePasswordForm.value;
    verificationCodeInfo.type = VerificationType.ChangePassword;
    this.verificationCodeService.checkCode(this.user.id, verificationCodeInfo)
      .subscribe(check => {
        //alert("check=" + check);
        if (check == true) {
          let user: User = this.changePasswordForm.value;
          this.profileService.changePasswordWithoutAuth(this.user.id, user)
            .subscribe(_ => {
              this.successfulCredentials = true;
              //alert("successfulCredentials=" + this.successfulCredentials);
              this.emailService.SendEmailWithCodeForChangingPassword(this.user.id).subscribe();
            });
        }
        else {
          this.wrongCode = true;
          setTimeout(function () { this.wrongCode = false }, 2000);
        }
      });
  }

  resetEmail() {
    this.isSetEmail = false;
  }

  get email(): AbstractControl {
    return this.emailForm.get('email');
  }

  get code(): AbstractControl {
    return this.changePasswordForm.get('code');
  }

  get password(): AbstractControl {
    return this.changePasswordForm.get('password');
  }

  get confirmPassword(): AbstractControl {
    return this.changePasswordForm.get('confirmPassword');
  }

  sendEmail() {
    this.emailForm.get('userId').setValue(this.user.id);
    let verificationCodeInfo: VerificationCode = this.emailForm.value;
    verificationCodeInfo.type = VerificationType.ChangePassword;

    console.log(verificationCodeInfo);
    this.verificationCodeService.create(verificationCodeInfo)
      .subscribe(code => {
        this.emailService.SendEmailWithCodeForChangingPassword(this.user.id).
          subscribe(_ => {
            this.isSetEmail = true;
          },
            err => {
              this.wrongSendEmail = true;
              setTimeout(function () { this.wrongCredentials = false }, 2000);
            })
      },
        err => {
          this.wrongCreateCode = true;
          setTimeout(function () { this.wrongCredentials = false }, 2000);
        });
  }
}
