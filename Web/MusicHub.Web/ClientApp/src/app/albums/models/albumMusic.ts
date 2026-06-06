import { Music } from "../../musics/models/music";
import Album from "./album";

export default interface AlbumMusic {
  id?: string,
  albumId?: string,
  album?: Album,
  music?: Music,
  musicId?: string,
}
