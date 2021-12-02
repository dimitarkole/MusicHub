import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BooleanSearchEntity, BooleanSearchInput } from '../../shared/models/boolean-search';
import { Music } from '../models/music';
import { MusicPagination } from '../models/musicPagination';
import { PlayedMusic } from '../models/playedMusic';
import { PlayedMusicPagination } from '../models/PlayedMusicPagination';

@Injectable({
  providedIn: 'root'
})
export class PlayedMusicService {
  private route: string = 'PlayedMusic';

  constructor(private http: HttpClient) { }

  all(page: number) {
    return this.http.get<PlayedMusicPagination>(`${this.route}/${page}`);
  }

  filter(page: number, music: Music) {
    return this.http.post<PlayedMusicPagination>(`${this.route}/FilterLikedMusics/${page}`, music);
  }

  delete(id: string) {
    return this.http.delete(`${this.route}/${id}`);
  }

  create(playedMusic: PlayedMusic) {
    return this.http.post<string>(this.route, playedMusic);
  }
}
