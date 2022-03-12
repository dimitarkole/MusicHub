import Playlist from './playlist';

export interface PlaylistPagination {
  playlists: Array<Playlist>,
  numberOfPages: number,
  currentPage: number,
}
