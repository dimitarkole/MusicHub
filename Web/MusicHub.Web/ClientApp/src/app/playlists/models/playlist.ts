import { OrderMethod } from "../../shared/models/orderMethod";
import User from "../../shared/models/user";
import { VisibleStatus } from "../../shared/models/visibleStatus";

export default interface Playlist {
  id: string,
  name: string,
  playlistSongsCount: number,
  user: User,
  userId: string,
  playlistId: string,
  songId: string,
  imageUrl: string,
  orderMethod: OrderMethod,
  visibleStatus: VisibleStatus
}
