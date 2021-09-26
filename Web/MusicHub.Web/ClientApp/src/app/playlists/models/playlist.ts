import { OrderMethod } from "../../shared/models/orderMethod";
import User from "../../shared/models/user";
import { PrivacyStatus } from "../../shared/models/visibleStatus";

export default interface Playlist {
  id: string,
  name: string,
  playlistMusicsCount: number,
  user: User,
  userId: string,
  playlistId: string,
  musicId: string,
  imageUrl: string,
  orderMethod: OrderMethod,
  privacy: PrivacyStatus,
}
