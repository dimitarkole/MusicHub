import { MusicLicenseType } from "../../shared/models/musicLicenseType";
import { OrderMethod } from "../../shared/models/orderMethod";
import { Reaction } from "../../shared/models/reaction";
import User from "../../shared/models/user";
import { VisibleStatus } from "../../shared/models/visibleStatus";

export interface Music {
  id: string,
  userId: string,
  name: string,
  imageUrl: string,
  audioUrl: string,
  categoryId: string,
  categoryName: string,
  description: string,
  countViews: string,
  countLikes: number,
  countDislikes: number,
  createdOn: Date,
  comments: Array<Comment>,
  licenses: Array<string>,
  //musicLicenses: Array<MusicLicense>,
  user: User,
  reaction: Reaction,
  orderMethod: OrderMethod,
  musicLicenseType: MusicLicenseType,
  visibleStatus: VisibleStatus
}
