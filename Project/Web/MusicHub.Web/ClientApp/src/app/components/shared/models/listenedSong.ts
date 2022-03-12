import { Song } from './song';

export default interface ListenedSong {
  id: string,
  song: Song,
  songId: string,
  createdOn: Date,
}
