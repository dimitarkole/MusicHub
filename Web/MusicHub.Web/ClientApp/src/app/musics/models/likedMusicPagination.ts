import { ReactionInfo } from "../../shared/models/reactionInfo";
import { Music } from "./music";

export interface LikedMusicPagination {
  linkedMusics: Array<ReactionInfo>,
  numberOfPages: number,
  currentPage: number,
}
