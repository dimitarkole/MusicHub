import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ReactionInfo } from '../../components/shared/models/reactionInfo';
import Comment from '../../components/shared/models/comment';

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
