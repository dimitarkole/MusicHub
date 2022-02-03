import Notification from "./notification";

export interface NotificationPagination {
  notifications: Array<Notification>,
  numberOfPages: number,
  currentPage: number,
}
