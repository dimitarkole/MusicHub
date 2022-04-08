import NotificationSetting from "./notificationSetting";

export interface NotificationPagination {
  notificationSetting: Array<NotificationSetting>,
  numberOfPages: number,
  currentPage: number,
}
