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
  successfulCredentials: boolean = false;

  constructor(
    private route: ActivatedRoute,
    public authService: AuthService) { }

  ngOnInit() {
    this.route.queryParams.subscribe(queryParams => {
      var model: VerificationCode = { code: queryParams.code, type: VerificationType.ConfirmEmail };
      this.user = this.route.snapshot.data.user;
      console.log(model);
      this.authService.confirmEmail(this.user.id, model).subscribe(() => {
        this.successfulCredentials = true;
      },
        err => {
          this.successfulCredentials = false;
          setTimeout(function () { this.wrongCredentials = false }, 2000);
        }
      );
    });
  }
}
