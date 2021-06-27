import { Permission } from "./permission";

export interface PermissionPagination {
  permissions: Permission[],
  numberOfPages: number,
  currentPage: number,
}
