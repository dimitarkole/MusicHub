import { Component, OnInit, Input } from '@angular/core';
import { FormGroup, FormBuilder, Validators, AbstractControl } from '@angular/forms';
import { Router } from '@angular/router';
import Comment from '../../../components/shared/models/comment';
import { modelConstants } from '../../../common/model-constants';
import { CommentService } from '../../../core/services/comment.service';
import { AuthService } from '../../../core/services/auth.service';

@Component({
  selector: 'app-comment-create',
  templateUrl: './comment-create.component.html',
  styleUrls: ['./comment-create.component.css']
})
export class CommentCreateComponent implements OnInit {
  isAuth: boolean = false;
  @Input() songId: string
  commentForm: FormGroup
  commentMinLength = modelConstants.comment.textMinLength
  commentMaxLength = modelConstants.comment.textMaxLength
  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private commentService: CommentService,
    private authService: AuthService) {
    this.authService.isAuthChanged.subscribe(() => {
      this.isAuth = this.authService.isAuth;
    })
  }

  ngOnInit() {
    this.commentForm = this.formBuilder.group({
      text: [
        null,
        [
          Validators.required,
          Validators.minLength(this.commentMinLength),
          Validators.maxLength(this.commentMaxLength)
        ]
      ]
    })
  }

  formHandler() {
    let comment: Comment = this.commentForm.value;
    comment.songId = this.songId;

    this.commentService.create(comment)
      .subscribe(_ => {
        this.router.navigate(['']);
      })

    this.commentForm.reset();
  }

  get text(): AbstractControl {
    return this.commentForm.get('text');
  }
}
