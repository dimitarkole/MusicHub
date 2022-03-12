import { Component, OnInit, Input } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Router } from '@angular/router';
import Comment from '../../../components/shared/models/comment';
import { AbstractControl, FormGroup, Validators, FormBuilder } from '@angular/forms';
import { Observable } from 'rxjs';
import { CommentDeleteComponent } from '../comment-delete/comment-delete.component';
import { CommentService } from '../../../core/services/comment.service';
import { AuthService } from '../../../core/services/auth.service';
import { modelConstants } from '../../../common/model-constants';

@Component({
  selector: 'app-comment-info',
  templateUrl: './comment-info.component.html',
  styleUrls: ['./comment-info.component.css']
})
export class CommentInfoComponent implements OnInit {
  @Input() comment: Comment;
  isAuth: boolean = false;
  isOwn: boolean = false;
  isEditing: boolean = false;
  editCommentForm: FormGroup
  commentMinLength = modelConstants.comment.textMinLength
  commentMaxLength = modelConstants.comment.textMaxLength

  constructor(private modalService: NgbModal,
    private formBuilder: FormBuilder,
    private commentService: CommentService,
    public authService: AuthService) {
    this.isAuth = authService.isAuth;
    this.authService.isAuthChanged.subscribe(() => {
      this.isAuth = this.authService.isAuth;
    });
  }

  ngOnInit(): void {
    this.editCommentForm = this.formBuilder.group({
      text: [
        this.comment.text,
        [
          Validators.required,
          Validators.minLength(this.commentMinLength),
          Validators.maxLength(this.commentMaxLength)
        ]
      ]
    });
    if (this.isAuth) {
      this.commentService.isOwn(this.comment.id)
        .subscribe(r => {
          this.isOwn = r;
        });
    }
  }
   
  openEdit() {
    this.isEditing = true;
  }

  cancelEditing() {
    this.isEditing = false;
  }

  formHandler() {
    let comment: Comment = this.editCommentForm.value;
    comment.id = this.comment.id;

    this.commentService.edit(comment)
      .subscribe(_ => {
        this.comment.text = comment.text;
        this.isEditing = false;
      })
  }

  openDelete() {
    let modal = this.modalService.open(CommentDeleteComponent);
    modal.result.then(value => {
      this.commentService.delete(this.comment.id).subscribe(_ => {
        this.comment = null;
      })
    }).catch(err => {
      console.log(err);
    })
  }

  get text(): AbstractControl {
    return this.editCommentForm.get('text');
  }
}
