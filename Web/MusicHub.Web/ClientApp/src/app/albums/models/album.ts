import { OrderMethod } from "../../shared/models/orderMethod";
import User from "../../shared/models/user";
import { PrivacyStatus } from "../../shared/models/visibleStatus";

export default interface Album {
  id: string,
  name: string,
  albumMusicsCount: number,
  user: User,
  userId: string,
  albumId: string,
  musicId: string,
  imageUrl: string,
  orderMethod: OrderMethod,
  privacy: PrivacyStatus,
}
