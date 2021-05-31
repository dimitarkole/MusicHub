import { Reaction } from "../../shared/models/reaction";
import User from "../../shared/models/user";

export default interface Comment {
  id: string,
  musicId: string,
  parentCommentId: string,
  text: string,
  likesCount: number,
  dislikesCount: number,
  commentsChildren: Array<Comment>,
  user: User,
  createdOn: Date,
  reaction: Reaction
}
