import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, AbstractControl } from '@angular/forms';
import { AuthService } from '../../core/services/auth.service';
import { Router } from '@angular/router';
import { ConfirmedValidator } from '../../common/confirmed.validator';
import VerificationCode from '../../components/shared/models/VerificationCode';
import { VerificationCodeService } from '../../core/services/verification-code.service';
import { EmailService } from '../../core/services/email.service';
import { ProfileService } from '../../core/services/profile.service';
import User from '../../components/shared/models/user';
import { getuid } from 'process';

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
      email: [null, [Validators.required, Validators.email]],
    })

    this.changePasswordForm = this.fb.group({
      email: [null, [Validators.required, Validators.email]],
      userName: [null, [Validators.required]],
      userId: [null, [Validators.required]],
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

    this.profileService.AdvancedSearch(user)
      .subscribe(getUsers => {
        console.log(getUsers);
        if ((getUsers == null) || (getUsers.length==0)) {
          this.wrongEmail = true;
          setTimeout(function () { this.wrongCredentials = false }, 2000);
        }
        else {
          this.user = getUsers[0];
          this.sendEmail();
        }
      },
        err => {
          this.wrongEmail = true;
        setTimeout(function () { this.wrongCredentials = false }, 2000);
      });
  }

  changePassword() {
    let verificationCodeInfo: VerificationCode = this.emailForm.value;

    this.verificationCodeService.checkCode(verificationCodeInfo)
      .subscribe(check => {
        if (check == true) {
          let user: User = this.changePasswordForm.value;
          this.profileService.changePasswordWithoutAuth(user)
            .subscribe(_ => {
              this.authService.login(this.userName.value, this.password.value)
                .subscribe((token: string) => {
                  this.successfulCredentials = true;
                  setTimeout(function () { this.successfulCredentials = false }, 2000);
                  this.authService.setToken(token);
                  this.authService.initializeAuthenticationState();
                  this.router.navigate(['/']);
                })
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

  get userName(): AbstractControl {
    return this.changePasswordForm.get('userName');
  }

  get password(): AbstractControl {
    return this.changePasswordForm.get('password');
  }

  get confirmPassword(): AbstractControl {
    return this.changePasswordForm.get('confirmPassword');
  }

  sendEmail() {
    this.changePasswordForm.get('email').setValue(this.email);
    this.changePasswordForm.get('userId').setValue(this.user.id);
    this.changePasswordForm.get('userName').setValue(this.user.userName);
    this.emailForm.get('userId').setValue(this.user.id);
    let verificationCodeInfo: VerificationCode = this.emailForm.value;

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
