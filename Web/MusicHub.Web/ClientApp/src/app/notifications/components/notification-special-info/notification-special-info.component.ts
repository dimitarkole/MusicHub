import { Component, OnInit, Input } from '@angular/core';
import { AuthService } from '../../../core/services/auth.service';
import { MusicService } from '../../../musics/services/music.service';
import NotificationClaim from '../../models/notificationClaim';
import { NotificationClaimType } from '../../models/notificationClaimType';

@Component({
  selector: 'app-notification-special-info',
  templateUrl: './notification-special-info.component.html',
  styleUrls: ['./notification-special-info.component.css']
})
export class NotificationSpecialInfoComponent implements OnInit {
  @Input() claim: NotificationClaim;
  @Input() type: string;
  userImageUrl: string;
  username: string;
  musicName: string;
  constructor(private authService: AuthService,
    private musicService: MusicService) { }

  ngOnInit() {
    if (this.claim.type == NotificationClaimType.UserId) {
      this.getUserData();
    }
    else if (this.claim.type == NotificationClaimType.MusicId) {
      this.getMusicData();
    }
  }

  getUserData() {
    this.authService.getUser(this.claim.content).subscribe(u => {
      this.userImageUrl = u.imageUrl;
      this.username = u.userName;
    });
  }

  getMusicData() {
    this.musicService.getById(this.claim.content).subscribe(m => {
      this.musicName = m.name.substring(0,10);
    });
  }
}
