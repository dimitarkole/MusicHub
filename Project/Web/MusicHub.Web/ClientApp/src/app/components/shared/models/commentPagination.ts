import Comment from './comment';

export interface CommentPagination {
  comments: Array<Comment>,
  numberOfPages: number,
  currentPage: number,
}
