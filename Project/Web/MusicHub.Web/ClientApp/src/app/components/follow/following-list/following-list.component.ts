import { Component, OnInit, SimpleChanges } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { FormBuilder } from '@angular/forms';
import { FollowService } from '../../../core/services/follow.service';
import { AuthService } from '../../../core/services/auth.service';
import { globalConstants } from '../../../common/global-constants';
import Follow from '../../shared/models/follow';
import getPage from '../../../common/paginator';

@Component({
  selector: 'app-following-list',
  templateUrl: './following-list.component.html',
  styleUrls: ['./following-list.component.css']
})
export class FollowingListComponent implements OnInit {
  userId: string;
  allFollowings: Follow[];
  page: number = globalConstants.pagination.defaultPage;
  itemsPerPage: number = globalConstants.pagination.itemsPerPage;
  followings: Follow[];
  isAuth: boolean;

  constructor(public modal: NgbActiveModal,
    private formBuilder: FormBuilder,
    private followService: FollowService,
    private authService: AuthService) {
    this.isAuth = authService.isAuth;
  }

  ngOnInit() {
    this.followService.GetFollowing(this.userId).subscribe(
      data => {
        this.allFollowings = data;
        this.getFollowingsPerPage(this.page);
      });
  }

  unfollow(id) {
    this.followService.delete(id)
      .subscribe(_ => {
        this.followService.GetFollowing(this.userId).subscribe(
          data => {
            this.allFollowings = data;
            this.getFollowingsPerPage(this.page);
          })
      });
  }

  public getFollowingsPerPage(page: number): void {
    this.followings = getPage<Follow>(this.allFollowings, page, this.itemsPerPage);
  }
}
