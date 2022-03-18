import { Music } from "../../musics/models/music";
import Playlist from "./playlist";

export default interface PlaylistMusic {
  id?: string,
  music?: Music,
  musicId?: string,
  playlistId?: string,
  playlist?: Playlist,
}
