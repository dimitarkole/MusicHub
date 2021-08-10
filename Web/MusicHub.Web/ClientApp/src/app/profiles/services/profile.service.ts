import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
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
    return this.http.post(`${this.route}/ChangePassword`, user);
  }

  changePasswordWithoutAuth(userId: string, user: User) {
    console.log(user);
    return this.http.post(`${this.route}/ChangePasswordWithoutAuth/${userId}`, user);
  }

  AdvancedSearch(user: User, page: number) {
    return this.http.post<User[]>(`${this.route}/AdvancedSearch/${page}`, user);
  }
}
