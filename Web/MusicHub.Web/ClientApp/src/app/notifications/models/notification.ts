import NotificationClaim from "./notificationClaim";
import { NotificationType } from "./notificationType";

export default interface Notification {
  id?: string,
  userId: string,
  content: string,
  seen?: boolean,
  createdOn?: Date,
  type: NotificationType,
  claims: NotificationClaim[],
  modifiedOn?: Date,
}
