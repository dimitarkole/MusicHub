import { ReactionInfo } from "../../shared/models/reactionInfo";
import { Music } from "./music";

export interface LikedMusicPagination {
  likedMusics: Array<ReactionInfo>,
  numberOfPages: number,
  currentPage: number,
}
