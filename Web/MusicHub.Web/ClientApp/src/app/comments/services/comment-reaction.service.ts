import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import Comment from '../models/comment';
import { ReactionInfo } from '../../shared/models/reactionInfo';

@Injectable({
  providedIn: 'root'
})
export class CommentReactionService {
  private route: string = 'CommentReaction';

  constructor(private http: HttpClient) { }

  reactionComment(reaction: Comment) {
    return this.http.post(this.route, reaction);
  }

  isReactedComment(CommentId: string) {
    return this.http.get<ReactionInfo>(`${this.route}/${CommentId}`);
  }

  delete(id: string) {
    return this.http.delete(`${this.route}/${id}`);
  }

  edit(comment: Comment, id: string) {
    return this.http.put(`${this.route}/${id}`, comment);
  }
}
