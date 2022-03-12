import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import Playlist from '../../components/shared/models/playlist';
import { Song } from '../../components/shared/models/song';
import { PlaylistPagination } from '../../components/shared/models/playlistPagination';

@Injectable({
  providedIn: 'root'
})
export class PlaylistService {
  private route: string = 'Playlist';
  private routeHomeController: string = 'Home';

  constructor(private http: HttpClient) { }

  getById(id: string) {
    return this.http.get<Playlist>(`${this.routeHomeController}/GetPlaylit/${id}`);
  }

  get(page: number) {
    return this.http.get<PlaylistPagination>(`${this.routeHomeController}/GetPlaylists/${page}`);
  }

  allOwn(page: number) {
    return this.http.get<PlaylistPagination>(`${this.route}/GetOwn/${page}`);
  }

  isOwn(id: string) {
    return this.http.get<boolean>(`${this.route}/IsOwn/${id}`);
  }

  getOwnForAddingSong(songId: string) {
    return this.http.get<Playlist[]>(`${this.route}/GetOwnForAddingSong/${songId}`);
  }

  create(playlist: Playlist) {
    return this.http.post(this.route, playlist);
  }

  edit(playlist: Playlist) {
    return this.http.put(`${this.route}/${playlist.id}`, playlist);
  }

  delete(id: string) {
    return this.http.delete(`${this.route}/${id}`);
  }

  allFilter(page: number, playlist: Playlist) {
    return this.http.post<PlaylistPagination>(`${this.routeHomeController}/PlaylistFilter/${page}`, playlist);
  }

  allFilterOwn(page: number, playlist: Playlist) {
    return this.http.post<PlaylistPagination>(`${this.route}/FilterOwn/${page}`, playlist);
  }
}
