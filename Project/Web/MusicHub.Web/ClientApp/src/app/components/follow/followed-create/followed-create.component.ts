import { Component, OnInit, Input, OnChanges, SimpleChanges } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { CategoryService } from '../../../core/services/category.service';
import { AuthService } from '../../../core/services/auth.service';
import { FollowService } from '../../../core/services/follow.service';
import Follow from '../../shared/models/follow';

@Component({
  selector: 'app-followed-create',
  templateUrl: './followed-create.component.html',
  styleUrls: ['./followed-create.component.css']
})
export class FollowedCreateComponent implements OnInit, OnChanges {
  @Input() userId: string;
  isAuth: Boolean = false;
  followForm: FormGroup;
  followId: string = "";
  isFollow: boolean = false;

  constructor(private formBuilder: FormBuilder,
    private followService: FollowService,
    private authService: AuthService) {
    this.isAuth = authService.isAuth;
    if (this.isAuth == true) {
       this.setFollowData();
    }
  }

  ngOnChanges(changes: SimpleChanges): void {
     this.setFollowData();
  }

  ngOnInit() {
    this.followForm = this.formBuilder.group({
      followedId: this.userId,
    });
    this.setFollowData();
  }

  submitFollow() {
    let follow: Follow = this.followForm.value;
    this.followService.create(follow)
      .subscribe(_ => this.setFollowData());
  }

  submitUnfollow() {
    let follow: Follow = this.followForm.value;
    this.followService.delete(this.followId)
      .subscribe(_ => this.setFollowData())
  }

  setFollowData() {
    this.followService.isFollowed(this.userId)
      .subscribe(f => {
          this.isFollow = f;
        if (f == true) {
          console.log("f + " + f);

          this.followService.getFollowId(this.userId)
            .subscribe(data => 
            {
              this.followId = data.id;
              console.log(this.followId);
            });
        }
      })
  }
}
