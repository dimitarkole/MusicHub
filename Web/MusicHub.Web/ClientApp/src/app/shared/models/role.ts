import { OrderMethod } from "./orderMethod";

export interface Role {
  id: string,
  name: string,
  permissionsCount: number,
  orderMethod: OrderMethod,
}
