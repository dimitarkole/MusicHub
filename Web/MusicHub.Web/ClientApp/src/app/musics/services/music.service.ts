import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
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
    //console.log(finalQuery);

    return this.http.post<TModel>(this.routeHomeController + "/" + entityName, { query: finalQuery });
  }


  allFilter(page: number, music: Music) {
    return this.http.get<MusicPagination>(`${this.routeHomeController}/Filter/${page}`, { params: this.setQueryString(music) });
  }

  suggestMusics(musicId: string, page: number) {
    return this.http.get<MusicPagination>(`${this.routeHomeController}/SuggestMusics/${musicId}/${page}`);
  }

  allOwn(page: number) {
    return this.http.get<MusicPagination>(`${this.routeMusicController}/GetOwn/${page}`);
  }

  isOwned(id: string) {
    return this.http.get<boolean>(`${this.routeMusicController}/IsOwned/${id}`);
  }

  allOwnFilter(page: number, music: Music) {
    return this.http.get<MusicPagination>(`${this.routeMusicController}/Filter/${page}`, {params:{ music: JSON.stringify(music) }});
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

  setQueryString(params: object) {
    let httpParams = new HttpParams();
    if (params) {
      for (const key of Object.keys(params)) {
        if (params[key])
        {
          httpParams = httpParams.set(key, params[key]);
        }
      }
    }
    return httpParams;
  }
}
