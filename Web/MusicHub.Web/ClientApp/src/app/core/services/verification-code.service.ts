import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import VerificationCode from '../../shared/models/verificationCode';

@Injectable({
  providedIn: 'root'
})
export class VerificationCodeService {
  private route: string = 'Verification';

  constructor(private http: HttpClient) { }

  create(verificationCode: VerificationCode) {
    return this.http.post(this.route, verificationCode);
  }

  getById(id: string) {
    return this.http.get<VerificationCode>(`${this.route}/${id}`);
  }

  getActivatedVerificationCode(id: string) {
    return this.http.get<VerificationCode>(`${this.route}/GetActivatedVerificationCode/${id}`);
  }

  checkCode(userId: string ,verificationCode: VerificationCode) {
    return this.http.post<boolean>(`${this.route}/CheckCode/${userId}`, verificationCode);
  }
}
