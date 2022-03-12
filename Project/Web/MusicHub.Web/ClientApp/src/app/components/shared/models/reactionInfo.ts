import { Reaction } from './reaction';
import { Song } from './song';

export interface ReactionInfo {
  id: string,
  reaction: Reaction,
  song: Song,
}
