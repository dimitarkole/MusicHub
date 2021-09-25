import { MusicLicenseType } from "../../shared/models/musicLicenseType";
import { OrderMethod } from "../../shared/models/orderMethod";
import { Reaction } from "../../shared/models/reaction";
import User from "../../shared/models/user";
import { PrivacyStatus } from "../../shared/models/visibleStatus";

export interface Music {
  id?: string,
  userId?: string,
  name?: string,
  imageUrl?: string,
  audioUrl?: string,
  categoryId?: string,
  categoryName?: string,
  description?: string,
  privacy?: PrivacyStatus,
  buyLink?: string,
  albumTitle?: string,
  recordLabel?: string,
  releaseDate?: Date,
  barcode?: string,
  iSWC?: string,
  pLine?: string,
  containsExplicitContent?: string,
  enableDirectDownloads?: boolean,
  displayEmbedCode?: boolean,
  offlineListening?: boolean,
  appPlayback?: boolean,
  rSSfeed?: boolean,
  containsMusic?: string,
  artist?: string,
  publisher?: string,
  iSRC?: string,
  composer?: string,
  releaseTitle?: string,
  license?: string,
  attribution?: boolean,
  noncommercial?: boolean,
  noDerivativeWorks?: boolean,
  shareALike?: boolean,

  countViews?: string,
  countLikes?: number,
  countDislikes?: number,
  createdOn?: Date,
  comments?: Array<Comment>,
  //licenses: Array<string>,
  //musicLicenses: Array<MusicLicense>,
  user?: User,
  reaction?: Reaction,
  orderMethod?: OrderMethod,
 // musicLicenseType: MusicLicenseType,
 // visibleStatus: VisibleStatus
}
