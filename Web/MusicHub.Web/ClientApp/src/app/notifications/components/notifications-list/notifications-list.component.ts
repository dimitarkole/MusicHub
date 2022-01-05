import { Component, HostListener, OnInit } from '@angular/core';
import { globalConstants } from '../../../common/global-constants';
import Notification from '../../models/notification';
import NotificationClaim from '../../models/notificationClaim';
import { NotificationClaimType } from '../../models/notificationClaimType';
import { NotificationPagination } from '../../models/notificationPagination';
import { NotificationType } from '../../models/notificationType';
import { NotificationService } from '../../services/notification.service';

@Component({
  selector: 'app-notifications-list',
  templateUrl: './notifications-list.component.html',
  styleUrls: ['./notifications-list.component.css']
})
export class NotificationsListComponent implements OnInit {
  notificationTypes: NotificationType;
  notificationPagination: NotificationPagination = { currentPage: 0, numberOfPages: 1, notifications: [] };

  constructor(private notificationService: NotificationService) {
    this.notificationPagination.notifications = new Array<Notification>();
  }

  ngOnInit(): void {
    this.getNotificationsPerPage();
  }

  onScroll() {
    //In chrome and some browser scroll is given to body tag
    var playlistSongsElement = document.getElementById("notifications");

    let pos = (playlistSongsElement.scrollTop || playlistSongsElement.scrollTop) + playlistSongsElement.offsetHeight;
    let max = playlistSongsElement.scrollHeight;
    // pos/max will give you the distance between scroll bottom and and bottom of screen in percentage.
    if (pos > max * globalConstants.pagination.updateProcent) {
      //Do your action here
      this.getNotificationsPerPage();
    }
  }

  private getNotificationsPerPage(): void {
    var page = this.notificationPagination.currentPage + 1;
    if (page <= this.notificationPagination.numberOfPages) {
      this.notificationPagination.currentPage = page;
      this.notificationService.get(page).subscribe(data => {
        this.notificationPagination.numberOfPages = data.numberOfPages;
        data.notifications.forEach(notification => {
          if (!notification.seen) {
            this.markAsSeen(notification.id);
          }
          this.notificationPagination.notifications.push(notification);
        });
      });
    }
  }

  isNotificationForLikeMusic(notificationType: NotificationType): boolean{
    return notificationType == NotificationType.LikeMusic;
  }

  getUserIdClaim(claims: NotificationClaim[])
  {
    return claims.filter(c => c.type == NotificationClaimType.UserId)[0];
  }

  getMusicIdClaim(claims: NotificationClaim[]) {
    return claims.filter(c => c.type == NotificationClaimType.MusicId)[0];
  }

  markAsSeen(notificationId: string) {
    this.notificationService.markAsSeen(notificationId).subscribe(data => {
    })
  }
}
