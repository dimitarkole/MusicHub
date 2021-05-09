import { Component, Input, OnInit, SimpleChanges } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { globalConstants } from '../../../common/global-constants';
import { AuthService } from '../../../core/services/auth.service';
import { Music } from '../../models/music';

@Component({
  selector: 'app-music-details',
  templateUrl: './music-details.component.html',
  styleUrls: ['./music-details.component.css']
})
export class MusicDetailsComponent implements OnInit {
  isAuth: boolean = false;
  @Input() music: Music;
  musicImageFolder: string = globalConstants.music.imageFolderPath;
  musicAudioFolder: string = globalConstants.music.audioFolderPath;
  musicId: string;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    public authService: AuthService) {
    this.isAuth = authService.isAuth;
    this.authService.isAuthChanged.subscribe(() => {
      this.isAuth = this.authService.isAuth;
    })
  }

  ngOnChanges(changes: SimpleChanges): void {
    this.route.params.subscribe(routeParams => {
      this.preparedPage();
    });
  }

  private preparedPage() {
    let player = <HTMLAudioElement>document.getElementById("player");
    player.src = globalConstants.music.audioFolderPath + this.music.audioUrl;
    player.play();
  }

  ngOnInit() {
    this.preparedPage();
  }

  /*openAddToSong() {
    let modal = this.modalService.open(PlaylistAddSongComponent);
    modal.componentInstance.song = this.song;
    modal.result.then(_ => {
    }).catch(err => {
      console.log(err);
    })
  }*/
}
