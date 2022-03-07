import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import Playlist from '../models/playlist';
import { PlaylistMusicsPagination } from '../models/playlistMusicsPagination';

@Injectable({
  providedIn: 'root'
})
export class PlaylistMusicsService {
  private route: string = 'PlaylistMusic';

  constructor(private http: HttpClient) { }

  create(playlist: Playlist) {
    return this.http.post(`${this.route}/Create`, playlist);
  }

  delete(songId: string) {
    return this.http.delete(`${this.route}/Delete/${songId}`);
  }

  all(playlistId: string, page: number) {
    return this.http.get<PlaylistMusicsPagination>(`${this.route}/Get/${playlistId}/${page}`);
  }
}
