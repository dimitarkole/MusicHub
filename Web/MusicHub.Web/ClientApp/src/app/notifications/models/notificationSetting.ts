import User from "../../shared/models/user";
import { NotificationSettingType } from "./notificationSettingType";

export default interface NotificationSetting {
  id?: string,
  type: NotificationSettingType,
  enabled?: boolean,
  userId?: string,
  user?: User,
}
