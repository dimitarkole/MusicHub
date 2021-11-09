import { ChartData } from "./chartData";
import { OrderMethod } from "./orderMethod";
import { Permission } from "./permission";
import { PermissionId } from "./permissionId";
import { RolePermission } from "./rolePermission";

export interface Chart<TValue> {
  title: string,
  data: ChartData<TValue>,
}
