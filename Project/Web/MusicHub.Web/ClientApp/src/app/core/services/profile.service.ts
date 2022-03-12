import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import User from '../../components/shared/models/user';

@Injectable({
  providedIn: 'root'
})
export class ProfileService { 
  private route: string = 'Profile';

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

  changePasswordWithoutAuth(user: User) {
    return this.http.post(`${this.route}/changePasswordWithoutAuth`, user);
  }

  AdvancedSearch(user: User) {
    return this.http.post<User[]>(`${this.route}/AdvancedSearch`, user);
  }
}
