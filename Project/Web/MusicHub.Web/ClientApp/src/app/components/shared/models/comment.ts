import User from './user';
import { Reaction } from './reaction';

export default interface Comment {
  id: string,
  songId: string,
  parentCommentId: string,
  text: string,
  likesCount: number,
  dislikesCount: number,
  commentsChildren: Array<Comment>,
  user: User,
  createdOn: Date,
  reaction: Reaction
}
