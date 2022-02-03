import { NotificationClaimType } from "./notificationClaimType";

export default interface NotificationClaim {
  id?: string,
  content: string,
  createdOn?: Date,
  type: NotificationClaimType,
  notificationId?: string;
}
