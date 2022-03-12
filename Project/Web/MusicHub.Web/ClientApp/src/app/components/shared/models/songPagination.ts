import Comment from './comment';
import User from './user';
import { Reaction } from './reaction';
import { OrderMethod } from './OrderMethod';
import { Song } from './song';

export interface SongPagination {
  songs: Array<Song>,
  numberOfPages: number,
  currentPage: number,
}
