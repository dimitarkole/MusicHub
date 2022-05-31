import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { setQueryString } from '../../common/QueryString';
import Playlist from '../models/playlist';
import PlaylistMusic from '../models/playlistMusic';
import { PlaylistMusicsPagination } from '../models/playlistMusicsPagination';

@Injectable({
  providedIn: 'root'
})
export class PlaylistMusicsService {
  private route: string = 'PlaylistMusic';

  constructor(private http: HttpClient) { }

  create(playlistMusic: PlaylistMusic) {
    return this.http.post(`${this.route}/Create`, playlistMusic);
  }

  getPlaylistMusicId(playlistMusic: PlaylistMusic) {
    return this.http.get<PlaylistMusic>(`${this.route}/GetPlaylistMusicId`, { params: setQueryString(playlistMusic) });
  }

  delete(playlisMusicId: string) {
    return this.http.delete(`${this.route}/Delete/${playlisMusicId}`);
  }

  all(playlistId: string, page: number) {
    return this.http.get<PlaylistMusicsPagination>(`${this.route}/Get/${playlistId}/${page}`);
  }

  isAddedToPlaylist(playlistMusic: PlaylistMusic) {
    return this.http.post<boolean>(`${this.route}/IsAddedToPlaylist`, { params: setQueryString(playlistMusic) });
  }
}  
