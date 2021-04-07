import { Music } from "./music";

export interface MusicPagination {
  musics: Array<Music>,
  numberOfPages: number,
  currentPage: number,
}
