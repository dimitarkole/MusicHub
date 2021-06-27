import { OrderMethod } from "./orderMethod";
import { Permission } from "./permission";
import { PermissionId } from "./permissionId";

export interface Role {
  id: string,
  name: string,
  permissionIds: PermissionId[],
  permissionsCount: number,
  permissions: Permission[],
  orderMethod: OrderMethod,
}
