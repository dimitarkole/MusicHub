import { OrderMethod } from "./orderMethod";
import { Permission } from "./permission";
import { PermissionId } from "./permissionId";
import { RolePermission } from "./rolePermission";

export interface ChartData<TValue> {
  key: string,
  value: TValue,
}
