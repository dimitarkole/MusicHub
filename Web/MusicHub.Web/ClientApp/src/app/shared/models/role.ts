import { OrderMethod } from "./orderMethod";
import { Permission } from "./permission";
import { PermissionId } from "./permissionId";
import { RolePermission } from "./rolePermission";

export interface Role {
  id: string,
  name: string,
  rolePermission: RolePermission[],
  permissionsPermission: Permission[],
  permissionIds: PermissionId[],
  permissionsCount: number,
  usersCount: number,
  permissions: RolePermission[],
  //permissions: Permission[],
  orderMethod: OrderMethod,
}
