import PlaylistSong from './playlistSong';

export interface PlaylistSongsPagination {
  playlistSongs: Array<PlaylistSong>,
  numberOfPages: number,
  currentPage: number,
}
