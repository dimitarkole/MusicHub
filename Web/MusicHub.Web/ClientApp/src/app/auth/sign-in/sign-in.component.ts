import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../core/services/auth.service';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css']
})
export class SignInComponent implements OnInit {
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

  login() {
    const { username, password } = this.loginForm.value;

    this.authService.login(username, password)
      .subscribe((token: string) => {
        this.authService.setToken(token);
        this.successfulCredentials = true;
        setTimeout(function () { this.successfulCredentials = false }, 2000);
        this.authService.initializeAuthenticationState();
        this.router.navigate(['/']);
      },
        err => {
          this.wrongCredentials = true;
          setTimeout(function () { this.wrongCredentials = false }, 2000);
        });
  }

  get username(): AbstractControl {
    return this.loginForm.get('username');
  }

  get password(): AbstractControl {
    return this.loginForm.get('password');
  }
}
