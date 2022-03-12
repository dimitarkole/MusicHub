import ListenedSong from './listenedSong';

export interface ListenedSongPagination {
  songViewHistory: Array<ListenedSong>,
  numberOfPages: number,
  currentPage: number,
}
