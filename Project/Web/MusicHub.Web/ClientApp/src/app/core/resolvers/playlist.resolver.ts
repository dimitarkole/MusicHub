import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import Playlist from '../../components/shared/models/playlist';
import { PlaylistService } from '../services/playlist.service';

@Injectable({
    providedIn: 'root'
})
export class PlaylistResolver implements Resolve<Playlist> {
  constructor(private playlistSercie: PlaylistService) { }

  resolve(route: ActivatedRouteSnapshot): Observable<Playlist> {
        let id = route.params['id'];
    return this.playlistSercie.getById(id);
   }
}
