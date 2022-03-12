import Comment from './comment';
import User from './user';
import { Reaction } from './reaction';
import { OrderMethod } from './OrderMethod';
import MusicLicense from './musicLicense';
import { MusicLicenseType } from './musicLicenseType';
import { VisibleStatus } from './visibleStatus';

export interface Song {
  id: string,
  userId: string,
  name: string,
  imageFilePath: string,
  audioFilePath: string,
  categoryId: string,
  categoryName: string,
  text: string,
  countViews: string,
  countLikes: number,
  countDislikes: number,
  createdOn: Date,
  comments: Array<Comment>,
  licenses: Array<string>,
  songLicenses: Array<MusicLicense>,
  user: User,
  reaction: Reaction,
  orderMethod: OrderMethod,
  musicLicenseType: MusicLicenseType,
  visibleStatus: VisibleStatus
}
