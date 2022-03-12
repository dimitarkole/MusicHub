import Playlist from './playlist';
import { Song } from './song';

export default interface User {
  id: string,
  username: string,
  userName: string,
  password: string,
  confirmPassword: string,
  currentPassword: string,
  newPassword: string,
  age: number,
  firstName: string,
  lastName: string,
  birthday: Date,
  email: string,
  avater: string,
  imageUrl: string,
  phone: string,
  songsCount: number,
  playlistsCount: number,
  playlists: Playlist[],
  songs: Song[],
}
