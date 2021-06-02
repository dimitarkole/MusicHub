import { Component, OnInit, SimpleChanges } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatDialog, MatDialogConfig } from '@angular/material';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { AuthService } from '../../../core/services/auth.service';
import User from '../../../shared/models/user';
import { ProfileService } from '../../services/profile.service';
import { ProfileChangePasswordComponent } from '../profile-change-password/profile-change-password.component';
import { ProfileEditComponent } from '../profile-edit/profile-edit.component';

@Component({
  selector: 'app-profile-own',
  templateUrl: './profile-own.component.html',
  styleUrls: ['./profile-own.component.css']
})
export class ProfileOwnComponent implements OnInit {
  user: User;
  userId: string;
  constructor(private modalService: NgbModal,
    private formBuilder: FormBuilder,
    public matDialog: MatDialog,
    private route: ActivatedRoute,
    private profileService: ProfileService,
    private router: Router,
    private authService: AuthService) {
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

  /*openFollowing() {
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
  }*/

  openEdit() {
    const dialogConfig = new MatDialogConfig();
    // The user can't close the dialog by clicking outside its body
    dialogConfig.id = "modal-component";
    dialogConfig.width = "600px";
    // https://material.angular.io/components/dialog/overview
    const modalDialog = this.matDialog.open(ProfileEditComponent, dialogConfig);
    modalDialog.afterClosed().subscribe(result => {
      if (result == true) window.location.reload();
    });
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
