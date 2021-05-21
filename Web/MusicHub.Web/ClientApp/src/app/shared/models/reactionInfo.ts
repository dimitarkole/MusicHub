import { Music } from "../../musics/models/music";
import { Reaction } from "../../shared/models/reaction";

export interface ReactionInfo {
  id: string,
  reaction: Reaction,
  music: Music,
}
