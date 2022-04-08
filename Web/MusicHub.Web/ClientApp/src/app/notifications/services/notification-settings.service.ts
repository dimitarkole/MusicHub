import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { NotificationPagination } from '../models/notificationPagination';
import NotificationSetting from '../models/notificationSetting';

@Injectable({
  providedIn: 'root'
})
export class NotificationSettingsService {
  private route: string = 'NotificationSettings';
  constructor(private http: HttpClient) { }

  create(notificationSetting: NotificationSetting) {
    return this.http.post<string>(this.route, notificationSetting);
  }

  get() {
    return this.http.get<NotificationPagination>(`${this.route}/Get`);
  }

  getByType(notificationSettingType: NotificationSetting) {
    return this.http.post<NotificationSetting>(`${this.route}/GetByType`, notificationSettingType);
  }

  isEnabled(notificationSettingType: NotificationSetting) {
    return this.http.post<boolean>(`${this.route}/IsEnabled`, notificationSettingType);
  }

  edit(id:string, notificationSetting: NotificationSetting) {
    return this.http.put(`${this.route}/${id}`, notificationSetting);
  }

  delete(id: string) {
    return this.http.delete(`${this.route}/${id}`);
  }
}
