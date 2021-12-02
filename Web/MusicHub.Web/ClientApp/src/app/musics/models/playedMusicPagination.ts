import { ReactionInfo } from "../../shared/models/reactionInfo";
import { Music } from "./music";
import { PlayedMusic } from "./playedMusic";

export interface PlayedMusicPagination {
  playedMusics: Array<PlayedMusic>,
  numberOfPages: number,
  currentPage: number,
}
