import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import Playlist from '../../playlists/models/playlist';
import { PlaylistService } from '../../playlists/services/playlist.service';

@Injectable({
  providedIn: 'root'
})
export class PlaylistResolver implements Resolve<Playlist> {
  constructor(private playlistService: PlaylistService) { }

  resolve(route: ActivatedRouteSnapshot): Observable<Playlist> {
    let id = route.params['id'];
    return this.playlistService.getById(id);
  }
}

