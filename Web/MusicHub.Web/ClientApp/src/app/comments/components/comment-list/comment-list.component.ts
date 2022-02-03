import { Component, HostListener, Input, OnInit, SimpleChanges } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { modelConstants } from '../../../common/model-constants';
import { CommentPagination } from '../../models/commentPagination';
import Comment from '../../models/comment';
import { CommentService } from '../../services/comment.service';
import { globalConstants } from '../../../common/global-constants';

@Component({
  selector: 'app-comment-list',
  templateUrl: './comment-list.component.html',
  styleUrls: ['./comment-list.component.css']
})
export class CommentListComponent implements OnInit {

  @Input() musicId: string;
  commentPagination: CommentPagination = { currentPage: 0, numberOfPages: 1, comments: [] };

  commentForm: FormGroup;
  commentMinLength = modelConstants.comment.textMinLength;
  commentMaxLength = modelConstants.comment.textMaxLength;

  constructor(//private modalService: NgbModal,
    private commentService: CommentService,
    private formBuilder: FormBuilder,
    private router: Router) {
  }

  ngOnChanges(changes: SimpleChanges): void {
    this.commentPagination.currentPage = 0;
    this.commentPagination.comments = [];
    this.getCommentsPerPage();
    this.commentForm = this.formBuilder.group({
      text: [
        null,
        [
          Validators.required,
          Validators.minLength(this.commentMinLength),
          Validators.maxLength(this.commentMaxLength)
        ]
      ]
    });
  }

  ngOnInit() {
    this.getCommentsPerPage();
    this.commentForm = this.formBuilder.group({
      text: [
        null,
        [
          Validators.required,
          Validators.minLength(this.commentMinLength),
          Validators.maxLength(this.commentMaxLength)
        ]
      ]
    });
  }

  formHandler() {
    let comment: Comment = this.commentForm.value;
    comment.musicId = this.musicId;

    this.commentService.create(comment)
      .subscribe(_ => {
        this.commentPagination.currentPage = 0;
        this.commentPagination.comments = [];
        this.getCommentsPerPage();
      });

    this.commentForm.reset();
  }

  @HostListener("window:scroll", [])
  onWindowScroll() {
    //In chrome and some browser scroll is given to body tag
    let pos = (document.documentElement.scrollTop || document.body.scrollTop) + document.documentElement.offsetHeight;
    let max = document.documentElement.scrollHeight;
    // pos/max will give you the distance between scroll bottom and and bottom of screen in percentage.
    if (pos > max * globalConstants.pagination.updateProcent) {
      //Do your action here
      this.getCommentsPerPage();
    }
  }

  get text(): AbstractControl {
    return this.commentForm.get('text');
  }

  private getCommentsPerPage(): void {
    var page = this.commentPagination.currentPage + 1;

    if (page <= this.commentPagination.numberOfPages) {
      this.commentPagination.currentPage = page;
      this.commentService.getByMusicId(this.musicId, page).subscribe(data => {
        this.commentPagination.numberOfPages = data.numberOfPages;
        data.comments.forEach(song => {
          if (!this.commentPagination.comments.some(s => s.id == song.id)) {
            this.commentPagination.comments.push(song);
          }
        });
      });
    }
  }
}
