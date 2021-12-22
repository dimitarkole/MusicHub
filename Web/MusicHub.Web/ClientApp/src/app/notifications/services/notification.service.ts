import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { NotificationPagination } from '../models/notificationPagination';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {
  private route: string = 'Notifications';

  constructor(private http: HttpClient) { }

  get(page: number) {
    return this.http.get<NotificationPagination>(`${this.route}/GetAll/${page}`);
  }

  markAsSeen(notificationId: string) {
    return this.http.put<NotificationPagination>(`${this.route}/MarkNotificationAsSeen/${notificationId}`, "");
  }
}
