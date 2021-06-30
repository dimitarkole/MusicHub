import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { ModalComponent } from 'ngb-modal';
import { globalConstants } from '../../../common/global-constants';
import { AuthService } from '../../../core/services/auth.service';
import { FollowPagination } from '../../models/followPagination';
import { FollowService } from '../../services/follow.service';

@Component({
  selector: 'app-followers-list',
  templateUrl: './followers-list.component.html',
  styleUrls: ['./followers-list.component.css']
})
export class FollowersListComponent implements OnInit {
  userId: string;
  followPagination: FollowPagination = { currentPage: 0, numberOfPages: 1, follows: [] };
  page: number = globalConstants.pagination.defaultPage;
  itemsPerPage: number = globalConstants.pagination.itemsPerPage;
  isAuth: boolean;

  constructor(@Inject(MAT_DIALOG_DATA) public data: any,
    public dialogRef: MatDialogRef<ModalComponent>,
    private followService: FollowService) {

  }

  ngOnInit() {
    this.followService.getUserFollowers(this.data, 1).subscribe(
      data => {
        this.followPagination = data;
      });
  }

  unfollow(id) {
    this.followService.delete(id)
      .subscribe(_ => {
        this.followService.getUserFollowers(this.userId, 0).subscribe(
          data => {
            this.followPagination = data;
          })
      });
  }

  // If the user clicks the cancel button a.k.a. the go back button, then\
  // just close the modal
  closeModal() {
    this.dialogRef.close();
  }

}
