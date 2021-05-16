import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import CommentDislike from '../models/commentDislike';
import CommentLike from '../models/commentLike';
import Comment from '../models/comment';
import { CommentPagination } from '../models/commentPagination';

@Injectable({
  providedIn: 'root'
})
export class CommentService {
  private routeCommentController: string = 'comment';
  private routeCommentLikeController: string = 'commentLike';
  private routeCommentDislikeController: string = 'commentDislike';
  constructor(private http: HttpClient) { }

  getById(id: string) {
    return this.http.get<Comment>(`${this.routeCommentController}/${id}`);
  }

  getByMusicId(id: string, page: number) {
    return this.http.get<CommentPagination>(`${this.routeCommentController}/${id}/${page}`);
  }

  create(comment: Comment) {
    return this.http.post(this.routeCommentController + '/post', comment);
  }

  isOwn(id: string) {
    return this.http.get<boolean>(`${this.routeCommentController}/IsOwn/${id}`);
  }

  createChildrenComment(comment: Comment) {
    return this.http.post(this.routeCommentController + '/postChildrenCommentar', comment);
  }

  edit(comment: Comment) {
    return this.http.put(`${this.routeCommentController}/${comment.id}`, comment);
  }

  delete(id: string) {
    return this.http.delete(`${this.routeCommentController}/${id}`);
  }

  likeComment(commentLike: CommentLike) {
    return this.http.post(this.routeCommentLikeController, { "commentId": commentLike.commentId });
  }

  dislikeComment(commentDislike: CommentDislike) {
    return this.http.post(this.routeCommentDislikeController, commentDislike);
  }
}
