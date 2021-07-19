import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { BooleanSearchEntity, BooleanSearchInput } from '../../shared/models/boolean-search';
import { Music } from '../models/music';
import { MusicPagination } from '../models/musicPagination';

@Injectable({
  providedIn: 'root'
})
export class MusicService {
  private routeMusicController: string = 'Music';
  private routeHomeController: string = 'Home';

  constructor(private http: HttpClient) { }

  all(page: number) {
    return this.http.get<MusicPagination>(`${this.routeHomeController}/Get/${page}`);
  }

  search<TModel>(page:number, query: BooleanSearchInput, booleanSearchEntity: BooleanSearchEntity): Observable<TModel> {

    let entityName = BooleanSearchEntity[booleanSearchEntity];
    let finalQuery = query.query.substring(query.query.indexOf(' ')).trim();
    console.log(finalQuery);

    return this.http.post<TModel>(this.routeHomeController + "/" + entityName, { query: finalQuery });
  }


  allFilter(page: number, music: Music) {
    return this.http.post<MusicPagination>(`${this.routeHomeController}/Filter/${page}`, music);
  }

  suggestSongs(musicId: string) {
    return this.http.get<Music[]>(`${this.routeHomeController}/SuggestSongs/${musicId}`);
  }

  allOwn(page: number) {
    return this.http.get<MusicPagination>(`${this.routeMusicController}/GetOwn/${page}`);
  }

  allOwnFilter(page: number, music: Music) {
    return this.http.post<MusicPagination>(`${this.routeMusicController}/Filter/${page}`, music);
  }

  create(music: Music) {
    return this.http.post<string>(this.routeMusicController, music);
  }

  getById(id: string) {
    return this.http.get<Music>(`${this.routeHomeController}/${id}`);
  }

  edit(music: Music, id: string) {
    return this.http.put(`${this.routeMusicController}/${id}`, music);
  }

  delete(id: string) {
    return this.http.delete(`${this.routeMusicController}/${id}`);
  }
}
