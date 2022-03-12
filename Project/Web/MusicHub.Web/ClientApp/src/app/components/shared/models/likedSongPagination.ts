import Comment from './comment';
import User from './user';
import { Reaction } from './reaction';
import { OrderMethod } from './OrderMethod';
import { Song } from './song';
import { ReactionInfo } from './reactionInfo';

export interface LikedSongPagination {
  linkedSongs: Array<ReactionInfo>,
  numberOfPages: number,
  currentPage: number,
}
