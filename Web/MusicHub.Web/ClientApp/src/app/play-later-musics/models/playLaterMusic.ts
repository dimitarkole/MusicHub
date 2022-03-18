import { Music } from "../../musics/models/music";

export interface PlayLaterMusic {
  id?: string,
  music: Music,
  position: number,
  musicId: string,
  createdOn: Date, 
}
