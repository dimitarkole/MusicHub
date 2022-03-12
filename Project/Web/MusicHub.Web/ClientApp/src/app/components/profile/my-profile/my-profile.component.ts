import { Component, OnInit, OnChanges, SimpleChanges } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import User from '../../shared/models/user';
import { FormBuilder } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { ProfileService } from '../../../core/services/profile.service';
import { AuthService } from '../../../core/services/auth.service';
import { ProfileChangePasswordComponent } from '../profile-change-password/profile-change-password.component';
import { ProfileEditComponent } from '../profile-edit/profile-edit.component';
import { FollowersListComponent } from '../../follow/followers-list/followers-list.component';
import { FollowingListComponent } from '../../follow/following-list/following-list.component';

@Component({
  selector: 'app-my-profile',
  templateUrl: './my-profile.component.html',
  styleUrls: ['./my-profile.component.css']
})
export class MyProfileComponent implements OnInit, OnChanges {
  user: User;
  userId: string;
  constructor(private modalService: NgbModal,
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private profileService: ProfileService,
    private router: Router,
    private authService: AuthService) {
    if (authService.isAuth == false) {
      this.router.navigate(['']);
    }

    this.profileService.MyProfile().subscribe(data => {
      this.user = data;
      this.userId = data.id;
    })

  }
    ngOnChanges(changes: SimpleChanges): void {
      this.profileService.MyProfile().subscribe(data => {
        this.user = data;
        this.userId = data.id;
      })
    }

  ngOnInit() {
  }

  openFollowing() {
    let modal = this.modalService.open(FollowingListComponent);
    modal.componentInstance.userId = this.userId;
    modal.result.then(_ => {
    })
  }

  openFollowers() {
    let modal = this.modalService.open(FollowersListComponent);
    modal.componentInstance.userId = this.userId;
    modal.result.then(_ => {
    })
  }

  openEdit() {
    let modal = this.modalService.open(ProfileEditComponent);
    modal.result.then(value => {
   
    }).catch(err => {
      console.log(err);
    })
  }

  openChangePassword() {
    let modal = this.modalService.open(ProfileChangePasswordComponent);

    modal.result.then(_ => {
      this.router.navigate(['/myProfile']);
    }).catch(err => {
      console.log(err);
    })
  }
}
