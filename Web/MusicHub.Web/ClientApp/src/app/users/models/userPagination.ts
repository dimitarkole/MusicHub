import User from '../../shared/models/user';

export interface UserPagination {
  users: Array<User>,
  numberOfPages: number,
  currentPage: number,
}
