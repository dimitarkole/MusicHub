import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Song } from '../../components/shared/models/song';
import ListenedSong from '../../components/shared/models/listenedSong';
import { ListenedSongPagination } from '../../components/shared/models/listenedSongPagination';

@Injectable({
  providedIn: 'root'
})

export class LisenedSongService {

  private route: string = 'SongViewHistory';

  constructor(private http: HttpClient) { }


  all(page: number) {
    return this.http.get<ListenedSongPagination>(`${this.route}/${page}`);
  }

  filter(song: Song, page: number) {
    return this.http.post<ListenedSongPagination>(`${this.route}/Filter/${page}`, song);
  }

  create(song: ListenedSong) {
    return this.http.post(this.route, song);
  }

  delete(listenedSongId: string) {
    return this.http.delete(`${this.route}/${listenedSongId}`);
  }
}
