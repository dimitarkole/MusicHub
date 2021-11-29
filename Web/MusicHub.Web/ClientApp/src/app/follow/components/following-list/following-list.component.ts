import { Component, HostListener, Inject, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { ModalComponent } from 'ngb-modal';
import { globalConstants } from '../../../common/global-constants';
import { AuthService } from '../../../core/services/auth.service';
import { FollowPagination } from '../../models/followPagination';
import { FollowService } from '../../services/follow.service';

@Component({
  selector: 'app-following-list',
  templateUrl: './following-list.component.html',
  styleUrls: ['./following-list.component.css']
})
export class FollowingListComponent implements OnInit {
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
    this.followPagination.currentPage = 0;
    this.feachData();
  }

  unfollow(id) {
    this.followService.delete(id)
      .subscribe(_ => {
        this.followPagination.currentPage = 0;
        this.feachData();
      });
  }

  @HostListener("window:scroll", [])
  onWindowScroll() {
    //In chrome and some browser scroll is given to body tag
    let pos = (document.documentElement.scrollTop || document.body.scrollTop) + document.documentElement.offsetHeight;
    let max = document.documentElement.scrollHeight;
    // pos/max will give you the distance between scroll bottom and and bottom of screen in percentage.
    if (pos > max * globalConstants.pagination.updateProcent) {
      //Do your action here
      this.feachData();
    }
  }

  private feachData(): void {
    if (this.followPagination.currentPage < this.followPagination.numberOfPages) {
      var page = this.followPagination.currentPage + 1;
      this.followService.getUserFollowing(this.data, page).subscribe(
        data => {
          this.followPagination = data;
        });
    }
  }

  // If the user clicks the cancel button a.k.a. the go back button, then\
  // just close the modal
  closeModal() {
    this.dialogRef.close();
  }
}
