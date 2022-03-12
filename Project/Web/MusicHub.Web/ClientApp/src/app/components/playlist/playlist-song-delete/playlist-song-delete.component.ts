import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Router } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';

@Component({
  selector: 'app-playlist-song-delete',
  templateUrl: './playlist-song-delete.component.html',
  styleUrls: ['./playlist-song-delete.component.css']
})
export class PlaylistSongDeleteComponent {
  constructor(public modal: NgbActiveModal,
    private router: Router,
    private authService: AuthService) {
    if (authService.isAuth == false){
      this.router.navigate(['']);
    }
  }

}
