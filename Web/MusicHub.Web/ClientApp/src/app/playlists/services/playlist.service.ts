import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { setQueryString } from '../../common/QueryString';
import Playlist from '../models/playlist';
import { PlaylistPagination } from '../models/playlistPagination';

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
    return this.http.get<boolean>(`${this.route}/IsOwned/${id}`);
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
    return this.http.get<PlaylistPagination>(`${this.routeHomeController}/PlaylistFilter/${page}`, { params: setQueryString(playlist) });
  }

  allFilterOwn(page: number, playlist: Playlist) {
    return this.http.get<PlaylistPagination>(`${this.route}/FilterOwn/${page}`, { params: setQueryString(playlist) });
  }
}
