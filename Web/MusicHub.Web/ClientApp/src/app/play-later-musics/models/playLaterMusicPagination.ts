import { ReactionInfo } from "../../shared/models/reactionInfo";
import { PlayLaterMusic } from "./playLaterMusic";

export interface PlayLaterMusicPagination {
  playLaterMusics: Array<PlayLaterMusic>,
  numberOfPages: number,
  currentPage: number,
}
