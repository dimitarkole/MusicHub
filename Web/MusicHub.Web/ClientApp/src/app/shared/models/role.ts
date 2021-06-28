import { OrderMethod } from "./orderMethod";
import { Permission } from "./permission";
import { PermissionId } from "./permissionId";

export interface Role {
  id: string,
  name: string,
  permissionsPermission: Permission[],
  permissionIds: PermissionId[],
  permissionsCount: number,
  usersCount: number,
  permissions: Permission[],
  orderMethod: OrderMethod,
}
