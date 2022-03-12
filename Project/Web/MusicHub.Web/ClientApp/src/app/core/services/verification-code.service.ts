import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import VerificationCode from '../../components/shared/models/VerificationCode';

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

  checkCode(verificationCode: VerificationCode) {
    return this.http.post<boolean>(`${this.route}/CheckCode`, verificationCode);
  }
}
