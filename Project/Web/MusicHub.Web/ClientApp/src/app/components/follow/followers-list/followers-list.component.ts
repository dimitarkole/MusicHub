import { Component, OnInit, Input } from '@angular/core';
import Follow from '../../shared/models/follow';
import { globalConstants } from '../../../common/global-constants';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { FormBuilder } from '@angular/forms';
import { FollowService } from '../../../core/services/follow.service';
import { AuthService } from '../../../core/services/auth.service';
import getPage from '../../../common/paginator';

@Component({
  selector: 'app-followers-list',
  templateUrl: './followers-list.component.html',
  styleUrls: ['./followers-list.component.css']
})
export class FollowersListComponent implements OnInit {
  userId: string;
  allFollowers: Follow[];
  page: number = globalConstants.pagination.defaultPage;
  itemsPerPage: number = globalConstants.pagination.itemsPerPage;
  followers: Follow[];
  isAuth: boolean;

  constructor(public modal: NgbActiveModal,
    private formBuilder: FormBuilder,
    private followService: FollowService,
    private authService: AuthService) {
    this.isAuth = authService.isAuth;
  }

  ngOnInit() {
    this.followService.GetFollowers(this.userId).subscribe(
      data => {
        this.allFollowers = data;
        this.getFollowersPerPage(this.page);
      });    
  }

  unfollow(id) {
    this.followService.delete(id)
      .subscribe(_ => {
        this.followService.GetFollowers(this.userId).subscribe(
          data => {
            this.allFollowers = data;
            this.getFollowersPerPage(this.page);
          });
      });
  }

  public getFollowersPerPage(page: number): void {
    this.followers = getPage<Follow>(this.allFollowers, page, this.itemsPerPage);
    console.log(this.followers);
  }
}
