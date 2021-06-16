import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ReactionInfo } from '../../shared/models/reactionInfo';
import { LikedMusicPagination } from '../models/likedMusicPagination';
import { Music } from '../models/music';

@Injectable({
  providedIn: 'root'
})
export class MusicReactionService {
  private route: string = 'MusicReaction';

  constructor(private http: HttpClient) { }

  allLikedMusic(page: number) {
    return this.http.get<LikedMusicPagination>(`${this.route}/GetLikedMusics/${page}`);
  }

  filterLikedMusic(page: number, music: Music) {
    return this.http.post<LikedMusicPagination>(`${this.route}/FilterLikedMusics/${page}`, music);
  }

  createReaction(reaction: Music) {
    return this.http.post(this.route, reaction);
  }

  isMusicReacted(musicId: string) {
    return this.http.get<ReactionInfo>(`${this.route}/GetOwnReaction/${musicId}`);
  }

  delete(id: string) {
    return this.http.delete(`${this.route}/${id}`);
  }

  edit(music: Music, id: string) {
    return this.http.put(`${this.route}/${id}`, music);
  }
}
