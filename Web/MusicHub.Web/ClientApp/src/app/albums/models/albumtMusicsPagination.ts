import AlbumMusic from "./albumMusic";

export interface AlbumMusicsPagination {
  albumMusics: Array<AlbumMusic>,
  numberOfPages: number,
  currentPage: number,
}
