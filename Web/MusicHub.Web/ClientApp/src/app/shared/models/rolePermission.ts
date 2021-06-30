import { Permission } from "./permission";
import { Role } from "./role";

export interface RolePermission{
  permission: Permission,
  permissionId: number;
  role: Role,
  roleId: string;
}
