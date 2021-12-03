import { Component, Input, OnInit, SimpleChanges } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { AuthService } from '../../../core/services/auth.service';
import Follow from '../../models/follow';
import { FollowService } from '../../services/follow.service';

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
      .subscribe(_ => this.setFollowData());
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
