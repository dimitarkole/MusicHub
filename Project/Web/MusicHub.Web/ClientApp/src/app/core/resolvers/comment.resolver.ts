import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { Song } from '../../components/shared/models/song';
import { CommentService } from '../services/comment.service';
import Comment from '../../components/shared/models/comment';

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
