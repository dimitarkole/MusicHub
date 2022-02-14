import PlaylistMusic from "./playlistMusic";

export interface PlaylistMusicsPagination {
  playlistMusics: Array<PlaylistMusic>,
  numberOfPages: number,
  currentPage: number,
}
