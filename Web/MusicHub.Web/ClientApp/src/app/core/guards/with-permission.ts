import { PermissionType } from "../../shared/models/permissionType"

export const withPermission = (permissionType: PermissionType): { permissionType: PermissionType } => {
    return { permissionType }
}
