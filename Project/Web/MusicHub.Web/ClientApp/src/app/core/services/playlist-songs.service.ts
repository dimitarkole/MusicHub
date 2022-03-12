import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import Playlist from '../../components/shared/models/playlist';
import { PlaylistSongsPagination } from '../../components/shared/models/playlistsongsPagination';

@Injectable({
  providedIn: 'root'
})
export class PlaylistSongsService {
  private route: string = 'PlaylistSong';

  constructor(private http: HttpClient) { }

  create(playlist: Playlist) {
    return this.http.post(`${this.route}/Create`, playlist);
  }

  delete(songId: string) {
    return this.http.delete(`${this.route}/DeletePlaylistSong/${songId}`);
  }

  //[HttpGet(nameof(Get) + "/{id}/{page}")]

  all(playlistId: string, page: number) {
    return this.http.get<PlaylistSongsPagination>(`${this.route}/Get/${playlistId}/${page}`);
  }
}
