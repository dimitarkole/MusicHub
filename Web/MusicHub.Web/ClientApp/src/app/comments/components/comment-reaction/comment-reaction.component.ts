import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';
import { MusicService } from '../../../musics/services/music.service';
import { NotificationService } from '../../../notifications/services/notification.service';
import { ProfileService } from '../../../profiles/services/profile.service';
import { Reaction } from '../../../shared/models/reaction';
import { ReactionInfo } from '../../../shared/models/reactionInfo';
import Comment from '../../models/comment';
import { CommentReactionService } from '../../services/comment-reaction.service';
import Notification from '../../../notifications/models/notification';
import { NotificationClaimType } from '../../../notifications/models/notificationClaimType';
import { NotificationType } from '../../../notifications/models/notificationType';

@Component({
  selector: 'app-comment-reaction',
  templateUrl: './comment-reaction.component.html',
  styleUrls: ['./comment-reaction.component.css']
})
export class CommentReactionComponent implements OnInit {
  @Input() comment: Comment
  reactionForm: FormGroup;
  reaction: ReactionInfo;
  isAuth: boolean = false;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private musicService: MusicService,
    private prifileService: ProfileService,
    private notificationService: NotificationService,
    private commentReactionService: CommentReactionService,
    public authService: AuthService) {
    this.isAuth = authService.isAuth;
    this.authService.isAuthChanged.subscribe(() => {
      this.isAuth = this.authService.isAuth;
    })
  }

  ngOnInit(): void {
    if (this.isAuth) {
      this.setReaction();
      this.reactionForm = this.formBuilder.group({
        commentId: this.comment.id,
        reaction: null,
      });
    }
  }

  likeComment() {
    this.reactionForm.get('reaction').setValue(Reaction.Like);
    let reaction: Comment = this.reactionForm.value;
    reaction.reaction = Reaction.Like;
    this.commentReactionService.reactionComment(reaction)
      .subscribe(_ => {
        this.comment.likesCount++;
        this.setReaction();
        this.musicService.getById(this.comment.musicId).subscribe(m => {
          this.prifileService.MyProfile().subscribe(u => {
            var notification: Notification = {
              content: " likes your comment on the music ",
              userId: m.user.id,
              claims: [
                {
                  content: u.id,
                  type: NotificationClaimType.UserId,
                },
                {
                  content: m.id,
                  type: NotificationClaimType.MusicId,
                }
              ],
              type: NotificationType.NewComment
            };

            this.notificationService.create(notification).subscribe(_ => {});
          });
        });
      });
  }

  dislikeComment() {
    this.reactionForm.get('reaction').setValue(Reaction.Dislike);
    let reaction: Comment = this.reactionForm.value;
    reaction.reaction = Reaction.Dislike;
    this.commentReactionService.reactionComment(reaction)
      .subscribe(_ => {
        this.comment.dislikesCount++;
        this.setReaction();
      });
  }

  unReactionComment(id: string, oldReacton: string) {
    this.commentReactionService.delete(id)
      .subscribe(_ => {
        this.reaction.reaction = Reaction.None;
        if (oldReacton == "like") {
          this.comment.likesCount--;
        }
        else if (oldReacton == "dislike") {
          this.comment.dislikesCount--;
        }
        this.setReaction();
      });
  }

  unLikeComment(id: string) {
    this.reactionForm.get('reaction').setValue(Reaction.Dislike);
    let reaction: Comment = this.reactionForm.value;
    reaction.reaction = Reaction.Dislike;
    this.commentReactionService.edit(reaction, id)
      .subscribe(_ => {
        this.comment.dislikesCount++;
        this.comment.likesCount--;
        this.setReaction();
      });
  }

  unDislikeComment(id: string) {
    this.reactionForm.get('reaction').setValue(Reaction.Dislike);
    let reaction: Comment = this.reactionForm.value;
    reaction.reaction = Reaction.Like;

    this.commentReactionService.edit(reaction, id)
      .subscribe(_ => {
        this.comment.likesCount++;
        this.comment.dislikesCount--;
        this.setReaction();
      });
  }

  get commentIdGet(): string {
    return this.comment.id;
  }

  setReaction() {
    this.commentReactionService.isReactedComment(this.commentIdGet).subscribe(data => {
      this.reaction = data
    });
  }
}
