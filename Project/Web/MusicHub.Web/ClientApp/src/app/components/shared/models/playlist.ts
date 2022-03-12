import { Song } from './song';
import User from './user';
import PlaylistSong from './playlistSong';
import { OrderMethod } from './OrderMethod';

export default interface Playlist {
  id: string,
  name: string,
  playlistSongsCount: number,
  user: User,
  userId: string,
  playlistId: string,
  songId: string,
  orderMethod: OrderMethod,
}
