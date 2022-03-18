import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import Album from '../models/album';
import { AlbumPagination } from '../models/albumPagination';

@Injectable({
  providedIn: 'root'
})
export class AlbumService {
  private route: string = 'Album';

  constructor(private http: HttpClient) { }

  getById(id: string) {
    return this.http.get<Album>(`${this.route}/GetAlbum/${id}`);
  }

  get(page: number) {
    return this.http.get<AlbumPagination>(`${this.route}/GetAlbums/${page}`);
  }

  allOwn(page: number) {
    return this.http.get<AlbumPagination>(`${this.route}/GetOwn/${page}`);
  }

  isOwn(id: string) {
    return this.http.get<boolean>(`${this.route}/IsOwned/${id}`);
  }

  getOwnForAddingMusic(musicId: string) {
    return this.http.get<Album[]>(`${this.route}/GetOwnForAddingMusic/${musicId}`);
  }

  create(album: Album) {
    return this.http.post(this.route, album);
  }

  edit(album: Album) {
    return this.http.put(`${this.route}/${album.id}`, album);
  }

  delete(id: string) {
    return this.http.delete(`${this.route}/${id}`);
  }

  allFilter(page: number, album: Album) {
    return this.http.post<AlbumPagination>(`${this.route}/AlbumsFilter/${page}`, album);
  }

  allFilterOwn(page: number, album: Album) {
    return this.http.post<AlbumPagination>(`${this.route}/FilterOwn/${page}`, album);
  }
}
