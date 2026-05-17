import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../core/services/auth.service';
import { SignInModel } from '../models/SignInModel';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css']
})
export class SignInComponent implements OnInit {
  twoFactorRequired: boolean = false;
  twoFactorCode: string = '';
  twoFactorUserId: string = '';
  loginForm: FormGroup;
  wrongCredentials: boolean = false;
  error = "";
  successfulCredentials: boolean = false;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) { }

  ngOnInit() {
    this.loginForm = this.fb.group({
      username: [null, [Validators.required]],
      password: [null, [Validators.required]]
    })
  }

  // login() {
  //   const { username, password } = this.loginForm.value;

  //   this.authService.login(username, password)
  //     .subscribe((token: string) => {
  //       this.authService.setToken(token);
  //       this.successfulCredentials = true;
  //       setTimeout(function () { this.successfulCredentials = false }, 2000);
  //       this.authService.initializeAuthenticationState();
  //       this.router.navigate(['/']);
  //     },
  //       err => {
  //         this.wrongCredentials = true;
  //         setTimeout(function () { this.wrongCredentials = false }, 2000);
  //       });
  // }

  login() {
    const { username, password } = this.loginForm.value;
    this.authService.login(username, password)
      .subscribe((res: SignInModel) => {
        // TWO FACTOR
        if (res.requiresTwoFactor) {
          this.twoFactorRequired = true;
          this.twoFactorUserId = res.userId;
          return;
        }

        // NORMAL LOGIN
        this.authService.setToken(res.token)
        this.successfulCredentials = true;
        this.authService.initializeAuthenticationState();
        this.router.navigate(['/']);
      }, err => {
        console.log(err);
        this.error = err.error;
        this.wrongCredentials = true;
      });
  }

  verifyTwoFactor() {
    const model = {
      code: this.twoFactorCode,
      type: 2 // VerificationType.TwoFactorLogin
    };

    this.authService.verifyTwoFactor(this.twoFactorUserId ? this.twoFactorUserId : '',model)
      .subscribe((res: any) => {
        this.authService.setToken(res.token);
        this.successfulCredentials = true;
        this.authService.initializeAuthenticationState();
        this.router.navigate(['/']);
      }, err => {
        this.error = "Wrong verification code.";
        this.wrongCredentials = true;
      });
  }

  get username(): AbstractControl {
    return this.loginForm.get('username');
  }

  get password(): AbstractControl {
    return this.loginForm.get('password');
  }
}
