import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { setQueryString } from '../../common/QueryString';
import User from '../../shared/models/user';

@Injectable({
  providedIn: 'root'
})
export class ProfileService {
  private route: string = 'Users';

  constructor(private http: HttpClient) { }

  MyProfile() {
    return this.http.get<User>(`${this.route}/MyProfile`);
  }

  getById(id: string) {
    return this.http.get<User>(`${this.route}/${id}`);
  }

  edit(user: User) {
    return this.http.put(`${this.route}/EditMyProfile`, user);
  }

  changePassword(user: User) {
    return this.http.put(`${this.route}/ChangePassword`, user);
  }

  changePasswordWithoutAuth(userId: string, user: User) {
    return this.http.put(`${this.route}/ChangePasswordWithoutAuth/${userId}`, user);
  }

  AdvancedSearch(user: User, page: number) {
    return this.http.get<User[]>(`${this.route}/AdvancedSearch/${page}`, { params: setQueryString(user) });
  }
}
