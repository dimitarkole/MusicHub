import { Component, Input, OnInit, SimpleChanges } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { AuthService } from '../../../core/services/auth.service';
import { NotificationClaimType } from '../../../notifications/models/notificationClaimType';
import { NotificationType } from '../../../notifications/models/notificationType';
import { NotificationService } from '../../../notifications/services/notification.service';
import { ProfileService } from '../../../profiles/services/profile.service';
import Follow from '../../models/follow';
import { FollowService } from '../../services/follow.service';
import Notification from '../../../notifications/models/notification';

@Component({
  selector: 'app-follow-create',
  templateUrl: './follow-create.component.html',
  styleUrls: ['./follow-create.component.css']
})
export class FollowCreateComponent implements OnInit {
  @Input() userId: string;
  //FollowingId
  @Input() followId: string;
  isAuth: Boolean = false;
  followForm: FormGroup;
  isFollow: boolean = false;

  constructor(private formBuilder: FormBuilder,
    private followService: FollowService,
    private prifileSerivice: ProfileService,
    private notificationService: NotificationService,
    private authService: AuthService) {
    this.isAuth = authService.isAuth;
  }

  ngOnInit() {
    this.setFollowData();
    this.followForm = this.formBuilder.group({
      followingId: this.userId,
    });
  }

  submitFollow() {
    let follow: Follow = this.followForm.value;
    this.followService.create(follow)
      .subscribe(_ => {
        this.setFollowData();
        this.prifileSerivice.MyProfile().subscribe(u => {
          var notification: Notification = {
            content: " follows you ",
            userId: follow.followingId,
            claims: [
              {
                content: u.id,
                type: NotificationClaimType.UserId,
              }
            ],
            type: NotificationType.NewFollow
          };
          this.notificationService.create(notification).subscribe(_ => _);
        });
      });
  }

  submitUnfollow() {
    this.followService.delete(this.followId)
      .subscribe(_ => {
        this.isFollow = false;
      })
  }
    
  setFollowData() {
    //this.followService.isFollowing(this.userId)
    //  .subscribe(f => {
    //    this.isFollow = f;
    //    if (f == true) {
    //      alert("here")
          this.followService.getFollowingId(this.userId)
            .subscribe(data => {
              this.followId = data["id"];
              this.isFollow = this.followId != "";
            });
       /* }
      })*/
  }
}
