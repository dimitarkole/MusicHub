import { Role } from '../../shared/models/role';

export interface RolePagination {
  roles: Array<Role>,
  numberOfPages: number,
  currentPage: number,
}
