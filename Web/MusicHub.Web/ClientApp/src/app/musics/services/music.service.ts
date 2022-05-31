import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { setQueryString } from '../../common/QueryString';
import { BooleanSearchEntity, BooleanSearchInput } from '../../shared/models/boolean-search';
import { Music } from '../models/music';
import { MusicPagination } from '../models/musicPagination';

@Injectable({
  providedIn: 'root'
})
export class MusicService {
  private route: string = 'Music';

  constructor(private http: HttpClient) { }

  all(page: number) {
    return this.http.get<MusicPagination>(`${this.route}/Get/${page}`);
  }

  search<TModel>(page:number, query: BooleanSearchInput, booleanSearchEntity: BooleanSearchEntity): Observable<TModel> {

    let entityName = BooleanSearchEntity[booleanSearchEntity];
    let finalQuery = query.query.substring(query.query.indexOf(' ')).trim();
    //console.log(finalQuery);

    return this.http.get<TModel>(this.route + "/" + entityName, { params: {query: finalQuery } });
  }

  allFilter(page: number, music: Music) {
    return this.http.get<MusicPagination>(`${this.route}/Filter/${page}`, { params: setQueryString(music) });
  }

  suggestMusics(musicId: string, page: number) {
    return this.http.get<MusicPagination>(`${this.route}/SuggestMusics/${musicId}/${page}`);
  }

  allOwn(page: number) {
    return this.http.get<MusicPagination>(`${this.route}/GetOwn/${page}`);
  }

  isOwned(id: string) {
    return this.http.get<boolean>(`${this.route}/IsOwned/${id}`);
  }

  allOwnFilter(page: number, music: Music) {
    return this.http.get<MusicPagination>(`${this.route}/Filter/${page}`, {params:{ music: JSON.stringify(music) }});
  }

  create(music: Music) {
    return this.http.post<string>(this.route, music);
  }

  getById(id: string) {
    return this.http.get<Music>(`${this.route}/${id}`);
  }

  edit(music: Music, id: string) {
    return this.http.put(`${this.route}/${id}`, music);
  }

  delete(id: string) {
    return this.http.delete(`${this.route}/${id}`);
  }
}
