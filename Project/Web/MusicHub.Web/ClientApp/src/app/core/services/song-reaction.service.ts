import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Song } from '../../components/shared/models/song';
import { Reaction } from '../../components/shared/models/reaction';
import { ReactionInfo } from '../../components/shared/models/reactionInfo';
import { LikedSongPagination } from '../../components/shared/models/likedSongPagination';

@Injectable({
  providedIn: 'root'
})
export class SongReactionService {
  private route: string = 'SongReaction';

  constructor(private http: HttpClient) { }

  allLikedSongs(page: number) {
    return this.http.get<LikedSongPagination>(`${this.route}/GetLikedSongs/${page}`);
  }

  filterLikedSongs(page: number, song: Song) {
    return this.http.post<LikedSongPagination>(`${this.route}/FilterLikedSongs/${page}`, song);
  }

  reactionSong(reaction: Song) {
    return this.http.post(this.route, reaction);
  }

  isReactedSong(songId: string) {
    return this.http.get<ReactionInfo>(`${this.route}/GetOwnReaction/${songId}`);
  }

  delete(id: string) {
    return this.http.delete(`${this.route}/${id}`);
  }

  edit(song: Song, id: string) {
    return this.http.put(`${this.route}/${id}`, song);
  }
}
