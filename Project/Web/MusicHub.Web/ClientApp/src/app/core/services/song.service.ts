import { Injectable } from '@angular/core';
import { HttpClient, HttpEventType } from '@angular/common/http';
import { Song } from '../../components/shared/models/song';
import { SongPagination } from '../../components/shared/models/songPagination';

@Injectable({
  providedIn: 'root'
})
export class SongService {
  private routeSongController: string = 'Song';
  private routeHomeController: string = 'Home';

  constructor(private http: HttpClient) { }

  all(page: number) {
    return this.http.get<SongPagination>(`${this.routeHomeController}/Get/${page}`);
  }

  allFilter(page: number, song: Song) {
    return this.http.post<SongPagination>(`${this.routeHomeController}/Filter/${page}`, song);
  }

  suggestSongs(songId: string) {
    return this.http.get<Song[]>(`${this.routeHomeController}/SuggestSongs/${songId}`);
  }

  allOwn(page: number) {
    return this.http.get<SongPagination>(`${this.routeSongController}/GetOwn/${page}`);
  }

  allOwnFilter(page: number, song: Song) {
    return this.http.post<SongPagination>(`${this.routeSongController}/Filter/${page}`, song);
  }

  create(song: Song) {
    return this.http.post<string>(this.routeSongController, song);
  }

  getById(id: string) {
    return this.http.get<Song>(`${this.routeHomeController}/${id}`);
  }

  edit(song: Song, id: string) {
    return this.http.put(`${this.routeSongController}/${id}`, song);
  }

  delete(id: string) {
    return this.http.delete(`${this.routeSongController}/${id}`);
  }
}
