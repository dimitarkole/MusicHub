import { OnChanges } from '@angular/core';
import { Component, OnInit, SimpleChanges } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';
import { Music } from '../../models/music';
import { PlayedMusic } from '../../models/playedMusic';
import { MusicService } from '../../services/music.service';
import { PlayedMusicService } from '../../services/played-music.service';

@Component({
  selector: 'app-music-play',
  templateUrl: './music-play.component.html',
  styleUrls: ['./music-play.component.css']
})
export class MusicPlayComponent implements OnInit {
  isAuth: boolean = false;
  playedMusicForm: FormGroup
  music: Music;
  musicId: string;

  constructor(
    private route: ActivatedRoute,
    private formBuilder: FormBuilder,
    private musicService: MusicService,
    private playedMusicService: PlayedMusicService,
    private authService: AuthService) {
    this.isAuth = authService.isAuth;
    this.authService.isAuthChanged.subscribe(() => {
      this.isAuth = this.authService.isAuth;
    })
  }

  ngOnChanges(changes: SimpleChanges): void {
    this.preparedPage();
  }

  private preparedPage() {
    this.playedMusicForm = this.formBuilder.group({
      musicId: null,
    });
    this.music = this.route.snapshot.data.music;
    this.musicId = this.music.id;
    //alert(this.musicId)
    if (this.isAuth) {
      this.playedMusicForm.get('musicId').setValue(this.musicId);
      let playedMusic: PlayedMusic = this.playedMusicForm.value;
      this.playedMusicService.create(playedMusic).subscribe();
    }
  }

  ngOnInit() {
    this.preparedPage();
  }
}
