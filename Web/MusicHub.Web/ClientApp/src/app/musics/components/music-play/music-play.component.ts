import { Component, OnInit, SimpleChanges } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';
import { Music } from '../../models/music';
import { MusicService } from '../../services/music.service';

@Component({
  selector: 'app-music-play',
  templateUrl: './music-play.component.html',
  styleUrls: ['./music-play.component.css']
})
export class MusicPlayComponent implements OnInit {
  isAuth: boolean = false;
  lisenedMusicForm: FormGroup
  music: Music;
  musicId: string;

  constructor(
    private route: ActivatedRoute,
    private formBuilder: FormBuilder,
    private musicService: MusicService,
    private authService: AuthService) {
    //private lisenedMusicService: LisenedMusicService
    this.isAuth = authService.isAuth;
    this.authService.isAuthChanged.subscribe(() => {
      this.isAuth = this.authService.isAuth;
    })
  }

  ngOnChanges(changes: SimpleChanges): void {
    this.preparedPage();
  }

  private preparedPage() {
    this.lisenedMusicForm = this.formBuilder.group({
      musicId: null,
    });
    this.music = this.route.snapshot.data.music;
    console.log("Here");
    console.log(this.music);
    this.musicId = this.music.id;
   
    if (this.isAuth) {
      this.lisenedMusicForm.get('musicId').setValue(this.musicId);
     // let lisenedSong: listenedSong = this.lisenedSongForm.value;
     /* this.lisenedSongService.create(lisenedSong)
        .subscribe(_ => _);*/
    }
  }

  ngOnInit() {
    this.preparedPage();
  }
}
