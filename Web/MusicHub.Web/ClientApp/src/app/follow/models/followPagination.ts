import Follow from './follow';

export interface FollowPagination {
  follows: Array<Follow>,
  numberOfPages: number,
  currentPage: number,
}
