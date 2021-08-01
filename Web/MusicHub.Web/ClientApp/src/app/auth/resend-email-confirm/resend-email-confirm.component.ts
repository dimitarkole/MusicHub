import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../core/services/auth.service';

@Component({
  selector: 'app-resend-email-confirm',
  templateUrl: './resend-email-confirm.component.html',
  styleUrls: ['./resend-email-confirm.component.css']
})
export class ResendEmailConfirmComponent implements OnInit {
  sendConfingEmailForm: FormGroup;
  wrongCredentials: boolean = false;
  error = "";
  successfulCredentials: boolean = false;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) { }

  ngOnInit() {
    this.sendConfingEmailForm = this.fb.group({
      email: [null, [Validators.required]],
    })
  }

  login() {
    const { email } = this.sendConfingEmailForm.value;

    this.authService.resendConfirmEmail(email)
      .subscribe((token: string) => {
        this.successfulCredentials = true;
      },
      err => {
        this.wrongCredentials = true;
        this.error = err.error;
        setTimeout(function () { this.wrongCredentials = false }, 2000);
      });
  }

  get email(): AbstractControl {
    return this.sendConfingEmailForm.get('email');
  }
}
