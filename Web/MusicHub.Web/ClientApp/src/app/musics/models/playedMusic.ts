import { Music } from "./music";

export interface PlayedMusic {
  id?: string,
  music: Music,
  musicId: string,
  createdOn: Date, 
}
