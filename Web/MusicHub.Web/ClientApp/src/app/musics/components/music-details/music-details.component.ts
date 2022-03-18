import { Component, Input, OnInit, SimpleChanges } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatDialog, MatDialogConfig } from '@angular/material';
import { ActivatedRoute, Router } from '@angular/router';
import { globalConstants } from '../../../common/global-constants';
import { AuthService } from '../../../core/services/auth.service';
import { Music } from '../../models/music';
import { AddMusicToPlaylistComponent } from '../add-music-to-playlist/add-music-to-playlist.component';
import { MusicDeleteComponent } from '../music-delete/music-delete.component';

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
  isAdding: boolean = false;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    public matDialog: MatDialog,
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
    player.src = this.musicAudioFolder + this.music.audioUrl;
    player.play();
  }

  ngOnInit() {
    this.preparedPage();
  }

  openAddMusic() {
    this.isAdding = !this.isAdding;
  }
}
