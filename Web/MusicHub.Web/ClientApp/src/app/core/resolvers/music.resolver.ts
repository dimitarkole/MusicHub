import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { Music } from '../../musics/models/music';
import { MusicService } from '../../musics/services/music.service';
//import { ProfileService } from '../services/profile.service';

@Injectable({
  providedIn: 'root'
})
export class MusicResolver implements Resolve<Music> {
  constructor(private musicService: MusicService) { }

  resolve(route: ActivatedRouteSnapshot): Observable<Music> {
    let id = route.params['id'];
    return this.musicService.getById(id);
  }
}

