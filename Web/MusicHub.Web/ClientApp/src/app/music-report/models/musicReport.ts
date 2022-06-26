import { Music } from '../../musics/models/music';
import { OrderMethod } from '../../shared/models/orderMethod';
import User from '../../shared/models/user';
import { ReportMusicType } from './musicReportType';

export default interface ReportMusic {

  id: string,
  type: ReportMusicType,
  musicId: string,
  seen: boolean,
  description: string,
  userId: string,
  createdOn: Date,
  user: User,
  music: Music,
}
