import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import AlbumMusic from '../models/albumMusic';
import { AlbumMusicsPagination } from '../models/albumtMusicsPagination';

@Injectable({
  providedIn: 'root'
})
export class AlbumMusicService {
  private route: string = 'AlbumMusic';

  constructor(private http: HttpClient) { }

  create(albumMusic: AlbumMusic) {
    return this.http.post(`${this.route}/Create`, albumMusic);
  }

  delete(albumMusicId: string) {
    return this.http.delete(`${this.route}/Delete/${albumMusicId}`);
  }

  all(albumId: string, page: number) {
    return this.http.get<AlbumMusicsPagination>(`${this.route}/Get/${albumId}/${page}`);
  }
}
