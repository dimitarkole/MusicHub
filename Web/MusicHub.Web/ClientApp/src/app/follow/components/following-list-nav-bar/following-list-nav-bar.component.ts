import { Component, HostListener, OnInit } from '@angular/core';
import { globalConstants } from '../../../common/global-constants';
import { AuthService } from '../../../core/services/auth.service';
import Follow from '../../models/follow';
import { FollowPagination } from '../../models/followPagination';
import { FollowService } from '../../services/follow.service';

@Component({
  selector: 'app-following-list-nav-bar',
  templateUrl: './following-list-nav-bar.component.html',
  styleUrls: ['./following-list-nav-bar.component.css']
})
export class FollowingListNavBarComponent implements OnInit {
  followPagination: FollowPagination = { currentPage: 0, numberOfPages: 1, follows: [] };
  page: number = globalConstants.pagination.defaultPage;
  itemsPerPage: number = globalConstants.pagination.itemsPerPage;
  isAuth: boolean;

  constructor(private followService: FollowService) {
  }

  ngOnInit() {
    this.feachData()
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
      this.followService.allOwnFollowing(page).subscribe(
        data => {
          this.followPagination = data;
        });
    }
  }
}
