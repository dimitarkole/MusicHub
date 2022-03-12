import { Component, OnInit, OnChanges, SimpleChanges } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { Song } from '../../shared/models/song';
import { SongService } from '../../../core/services/song.service';
import { LisenedSongService } from '../../../core/services/lisened-song.service';
import { AuthService } from '../../../core/services/auth.service';
import listenedSong from '../../shared/models/listenedSong';
import { SongResolver } from '../../../core/resolvers/song.resolver';

@Component({
  selector: 'app-play',
  templateUrl: './play.component.html',
  styleUrls: ['./play.component.css']
})
export class PlayComponent implements OnInit, OnChanges {
  isAuth: boolean = false;
  lisenedSongForm: FormGroup
  song: Song;
  songId: string;

  constructor(
    private route: ActivatedRoute,
    private formBuilder: FormBuilder,
    private songService: SongService,
    private authService: AuthService,
    private lisenedSongService: LisenedSongService) {
    this.isAuth = authService.isAuth;
    this.authService.isAuthChanged.subscribe(() => {
      this.isAuth = this.authService.isAuth;
    })
  }

  ngOnChanges(changes: SimpleChanges): void {
    this.preparedPage();
  }

  private preparedPage() {
    this.lisenedSongForm = this.formBuilder.group({
      songId: null,
    });
    SongResolver
    this.song = this.route.snapshot.data.song;
    this.songId = this.song.id;
    if (this.isAuth) {
      this.lisenedSongForm.get('songId').setValue(this.songId);
      let lisenedSong: listenedSong = this.lisenedSongForm.value;
      this.lisenedSongService.create(lisenedSong)
        .subscribe(_ => _);
    } 
  }

  ngOnInit() {
    this.preparedPage();
  }
}
