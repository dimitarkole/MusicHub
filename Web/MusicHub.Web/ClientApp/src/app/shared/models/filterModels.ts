import { OrderMethod } from "./orderMethod";

export interface UserFilter {
  lastName: string;
  email: string;
  username: string;
  firstName: string;
  orderMethod: OrderMethod;
  middleName: string;
  initials: string;
  billingRate: number;
  activeStatus: string;
  barDate: string | null;
}
