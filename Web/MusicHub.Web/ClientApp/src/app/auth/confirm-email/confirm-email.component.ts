import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AuthService } from '../../core/services/auth.service';
import User from '../../shared/models/user';
import VerificationCode from '../../shared/models/verificationCode';
import { VerificationType } from '../../shared/models/verificationType';

@Component({
  selector: 'app-confirm-email',
  templateUrl: './confirm-email.component.html',
  styleUrls: ['./confirm-email.component.css']
})
export class ConfirmEmailComponent implements OnInit {
  user: User;
  successfulCredentials = false;
  wrongCode = false;

  code: string;
  userId: string;

  constructor(
    private route: ActivatedRoute,
    public authService: AuthService
  ) { }

  ngOnInit() {
    this.userId = this.route.snapshot.paramMap.get('id');
  }

  confirm() {
    const model: VerificationCode = {
      code: this.code,
      type: VerificationType.ConfirmEmail
    };

    this.authService.confirmEmail(this.userId, model)
      .subscribe((res: any) => {
        this.successfulCredentials = true;

        if (res && res.token) {
          this.authService.setToken(res.token);
          this.authService.initializeAuthenticationState();
        }

      }, err => {
        this.wrongCode = true;
      });
  }
}
