import { Component, HostListener, Input, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { modelConstants } from '../../../common/model-constants';
import { CommentService } from '../../services/comment.service';
import Comment from '../../models/comment';
import { AuthService } from '../../../core/services/auth.service';
import { CommentDeleteComponent } from '../comment-delete/comment-delete.component';
import { CommentPagination } from '../../models/CommentPagination';
import { globalConstants } from '../../../common/global-constants';

@Component({
  selector: 'app-comment-details',
  templateUrl: './comment-details.component.html',
  styleUrls: ['./comment-details.component.css']
})
export class CommentDetailsComponent implements OnInit {
  viewChilderen: boolean = false;
  @Input() comment: Comment;
  @Input() type: string;
  childrenCommentPagination: CommentPagination = { currentPage: 0, numberOfPages: 1, comments: [] };
  answersCount: number = 0;
  isAuth: boolean = false;
  isOwn: boolean = false;
  isEditing: boolean = false;
  editCommentForm: FormGroup;
  commentChildrenForm: FormGroup;
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
    this.getCommentsPerPage();
    this.commentChildrenForm = this.formBuilder.group({
      text: [
        null,
        [
          Validators.required,
          Validators.minLength(this.commentMinLength),
          Validators.maxLength(this.commentMaxLength)
        ]
      ],
      parentCommentId: this.comment.id,
        
    });

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

  viewChilder() {
    this.viewChilderen = !this.viewChilderen;
    if (this.viewChilderen) {
      this.childrenCommentPagination.currentPage = 0;
      this.getCommentsPerPage();
    }
  }

  formHandler() {
    let comment: Comment = this.editCommentForm.value;
    comment.id = this.comment.id;
    comment.musicId = this.comment.musicId;
    this.commentService.edit(comment)
      .subscribe(_ => {
        this.comment.text = comment.text;
        this.isEditing = false;
      })
  }

  createChildrenComment() {
    let comment: Comment = this.commentChildrenForm.value;

    this.commentService.createChildrenComment(this.comment.id, comment)
      .subscribe(_ => {
        /*this.childrenCommentPagination.currentPage = 0;
        this.childrenCommentPagination.comments = [];
        this.getCommentsPerPage();*/
      });
    this.commentChildrenForm.reset();
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

  private getCommentsPerPage(): void {
    var page = this.childrenCommentPagination.currentPage + 1;
    if (page <= this.childrenCommentPagination.numberOfPages) {
      this.childrenCommentPagination.currentPage = page;
      this.commentService.getChildrenComments(this.comment.id, page).subscribe(data => {
        this.childrenCommentPagination.numberOfPages = data.numberOfPages;
        data.comments.forEach(song => {
          if (!this.childrenCommentPagination.comments.some(s => s.id == song.id)) {
            this.childrenCommentPagination.comments.push(song);
          }
        });
      });
    }
  }

  get text(): AbstractControl {
    return this.editCommentForm.get('text');
  }

  get textChildren(): AbstractControl {
    return this.commentChildrenForm.get('text');
  }
}
