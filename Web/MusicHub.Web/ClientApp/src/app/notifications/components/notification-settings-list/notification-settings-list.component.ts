import { Component, OnInit } from '@angular/core';
import NotificationSetting from '../../models/notificationSetting';
import { NotificationSettingType } from '../../models/notificationSettingType';
import { NotificationType } from '../../models/notificationType';
import { NotificationSettingsService } from '../../services/notification-settings.service';

@Component({
  selector: 'app-notification-settings-list',
  templateUrl: './notification-settings-list.component.html',
  styleUrls: ['./notification-settings-list.component.css']
})
export class NotificationSettingsListComponent implements OnInit {
  isLikeMusicChecked: boolean = false;
  isLikeMusicCommentChecked: boolean = false;
  isNewFollowersChecked: boolean = false;
  isNewCommentChecked: boolean = false;
  isResponseCommentChecked: boolean = false;
  constructor(private notificationSettingsService: NotificationSettingsService) {
    this.isChecked({ type: NotificationSettingType.NewFollower }).subscribe(r => this.isNewFollowersChecked = r);
    this.isChecked({ type: NotificationSettingType.LikeMusic }).subscribe(r => this.isLikeMusicChecked = r);
    this.isChecked({ type: NotificationSettingType.LikeComment }).subscribe(r => this.isLikeMusicCommentChecked = r);
    this.isChecked({ type: NotificationSettingType.NewComment }).subscribe(r => this.isNewCommentChecked = r);
    this.isChecked({ type: NotificationSettingType.ResponseComment }).subscribe(r => this.isResponseCommentChecked = r);
  }

  ngOnInit() {
  }

  isChecked(notificationSettingsType: NotificationSetting) {
    return this.notificationSettingsService.isEnabled(notificationSettingsType);
  }

  changeNewFollowers() {
    this.changeNotificationSetting(NotificationSettingType.NewFollower, !this.isNewFollowersChecked);
    this.isNewFollowersChecked = !this.isNewFollowersChecked;
  }

  changeComment() {
    this.changeNotificationSetting(NotificationSettingType.NewComment, !this.isNewCommentChecked);
    this.isNewCommentChecked = !this.isNewCommentChecked;
  }

  changeLikeComment() {
    this.changeNotificationSetting(NotificationSettingType.LikeComment, !this.isLikeMusicCommentChecked);
    this.isLikeMusicCommentChecked = !this.isLikeMusicCommentChecked;
  }

  changeLikeMusic() {
    this.changeNotificationSetting(NotificationSettingType.LikeMusic, !this.isLikeMusicChecked);
    this.isLikeMusicChecked = !this.isLikeMusicChecked;
  }

  changeResponseComment() {
    this.changeNotificationSetting(NotificationSettingType.ResponseComment, !this.isResponseCommentChecked);
    this.isResponseCommentChecked = !this.isResponseCommentChecked;
  }

  private changeNotificationSetting(type: NotificationSettingType, enabled: boolean) {
    var notificationSettingsType: NotificationSetting = { type: type, enabled: enabled };
    this.notificationSettingsService.getByType(notificationSettingsType).subscribe(r => {
        if (r) {
            this.notificationSettingsService.edit(r.id, notificationSettingsType).subscribe(r => r);
        }
        else {
            this.notificationSettingsService.create(notificationSettingsType).subscribe(r => r);
        }
    });
  }
}
