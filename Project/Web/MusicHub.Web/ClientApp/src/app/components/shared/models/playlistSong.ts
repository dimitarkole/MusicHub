import { Song } from './song';
import User from './user';

export default interface PlaylistSong {
  id: string,
  song: Song,
}
