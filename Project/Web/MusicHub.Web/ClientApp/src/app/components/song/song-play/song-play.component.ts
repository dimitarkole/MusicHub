import { Component, OnInit, Input, OnChanges, SimpleChanges } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { FormBuilder } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { PlaylistAddSongComponent } from '../../playlist/playlist-add-song/playlist-add-song.component';
import { AuthService } from '../../../core/services/auth.service';
import { Song } from '../../shared/models/song';

@Component({
  selector: 'app-song-play',
  templateUrl: './song-play.component.html',
  styleUrls: ['./song-play.component.css']
})
export class SongPlayComponent implements OnInit, OnChanges {
  isAuth: boolean = false;
  @Input() song: Song;
  songId: string;

  constructor(
    private modalService: NgbModal,
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
    this.song = this.route.snapshot.data.song;
      this.songId = this.song.id;
      let player = <HTMLAudioElement>document.getElementById("player");
      player.src = "../../../../assets/resources/song/аudio/" + this.song.audioFilePath;
      player.play();
  }

  ngOnInit() {
    this.preparedPage();
  }

  openAddToSong() {
    let modal = this.modalService.open(PlaylistAddSongComponent);
    modal.componentInstance.song = this.song;
    modal.result.then(_ => {
    }).catch(err => {
      console.log(err);
    })
  }
}
