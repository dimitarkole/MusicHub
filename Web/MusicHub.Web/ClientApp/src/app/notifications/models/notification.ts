export default interface Notification {
  id: string,
  userId: string,
  content: string,
  seen: boolean,
  createdOn: Date,
  modifiedOn: Date,
}
