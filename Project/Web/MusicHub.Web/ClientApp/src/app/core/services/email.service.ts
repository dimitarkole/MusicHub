import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import Email from '../../components/shared/models/email';

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
    console.log(id);
    return this.http.get(`${this.route}/SendEmailWithCodeForChangingPassword/${id}`);
  }
}
