import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import Comment from '../../comments/models/comment';
import { CommentService } from '../../comments/services/comment.service';

@Injectable({
    providedIn: 'root'
})
export class CommentResolver implements Resolve<Comment> {
  constructor(private commentService: CommentService) { }

    resolve(route: ActivatedRouteSnapshot): Observable<Comment> {
      let id = route.params['id'];
      return this.commentService.getById(id);
    }
}
