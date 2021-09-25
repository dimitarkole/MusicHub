import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import Email from '../../shared/models/email';

@Injectable({
  providedIn: 'root'
})
export class EmailService {
  private route: string = 'Email';

  constructor(
    private http: HttpClient
  ) { }

  SendEmail(email: Email) {
    return this.http.post(this.route, email);
  }

  SendEmailWithCodeForChangingPassword(id: string) {
    return this.http.get(`${this.route}/SendEmailWithCodeForChangingPassword/${id}`);
  }

  SendSuccessfulChangedPasswordEmail(id: string) {
    return this.http.get(`${this.route}/SendSuccessfulChangedPasswordEmail/${id}`);
  }
  
}
