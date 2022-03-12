import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { Song } from '../../components/shared/models/song';
import { SongService } from '../services/song.service';

@Injectable({
    providedIn: 'root'
})
export class SongResolver implements Resolve<Song> {
    constructor(private songService: SongService) { }

    resolve(route: ActivatedRouteSnapshot): Observable<Song> {
        let id = route.params['id'];
      return this.songService.getById(id);
    }
}
