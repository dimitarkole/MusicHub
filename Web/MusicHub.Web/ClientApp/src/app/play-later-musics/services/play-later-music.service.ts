import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { setQueryString } from '../../common/QueryString';
import { Music } from '../../musics/models/music';
import { PlayLaterMusic } from '../models/playLaterMusic';
import { PlayLaterMusicPagination } from '../models/playLaterMusicPagination';

@Injectable({
  providedIn: 'root'
})
export class PlayLaterMusicService {
  private route: string = 'PlayLaterMusics';

  constructor(private http: HttpClient) { }

  all(page: number) {
    return this.http.get<PlayLaterMusicPagination>(`${this.route}/${page}`);
  }

  filter(page: number, music: Music) {
    return this.http.get<PlayLaterMusicPagination>(`${this.route}/FilterLikedMusics/${page}`, { params: setQueryString(music) });
  }

  delete(id: string) {
    return this.http.delete(`${this.route}/${id}`);
  }

  create(playedMusic: PlayLaterMusic) {
    return this.http.post<string>(this.route, playedMusic);
  }

  edit(id: string, playLaterMusic: PlayLaterMusic) {
    return this.http.put<string>(`${this.route}/${id}`, playLaterMusic);
  }
}
