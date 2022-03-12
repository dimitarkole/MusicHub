import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, AbstractControl } from '@angular/forms';
import { AuthService } from 'src/app/core/services/auth.service';
import { Router, Data } from '@angular/router';
import { ConfirmedValidator } from '../../common/confirmed.validator';
import User from '../../components/shared/models/user';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})

export class SignupComponent implements OnInit {
  signUpForm: FormGroup;
  wrongCredentials: boolean = false;
  successfulCredentials: boolean = false;

  maxDate: number;
  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
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
      birthday: [null, [Validators.required]],
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
    this.authService.create(newUser)
      .subscribe(_ => {
        this.successfulCredentials = true;
        setTimeout(function () { this.successfulCredentials = false }, 2000);
        this.authService.login(newUser.username, newUser.password)
          .subscribe((token: string) => {
            this.authService.setToken(token);
            this.authService.initializeAuthenticationState();
            this.router.navigate(['/']);
          },
          err => {
            this.wrongCredentials = true;
            setTimeout(function () { this.wrongCredentials = false }, 2000);
          });
      },
      err => {
        this.wrongCredentials = true;
        setTimeout(function () { this.wrongCredentials = false }, 2000);
      });
  }

  get password(): AbstractControl {
    return this.signUpForm.get('password');
  }

  get email(): AbstractControl{
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

  get birthday(): AbstractControl{
    return this.signUpForm.get('birthday'); 
  }

  get privacy(): AbstractControl {
    return this.signUpForm.get('privacy');
  }
}
