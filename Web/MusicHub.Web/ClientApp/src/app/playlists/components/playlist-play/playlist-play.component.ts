import { Component, HostListener, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { globalConstants } from '../../../common/global-constants';
import { AuthService } from '../../../core/services/auth.service';
import { Music } from '../../../musics/models/music';
import { PlayedMusic } from '../../../musics/models/playedMusic';
import { MusicService } from '../../../musics/services/music.service';
import { PlayedMusicService } from '../../../musics/services/played-music.service';
import Playlist from '../../models/playlist';
import { PlaylistMusicsPagination } from '../../models/playlistMusicsPagination';
import { PlaylistMusicsService } from '../../services/playlist-musics.service';
import { PlaylistService } from '../../services/playlist.service';

@Component({
  selector: 'app-playlist-play',
  templateUrl: './playlist-play.component.html',
  styleUrls: ['./playlist-play.component.css']
})
export class PlaylistPlayComponent implements OnInit {
  playlistMusicsPagination: PlaylistMusicsPagination = { currentPage: 0, numberOfPages: 1, playlistMusics: [] };
  isAuth: boolean = false;
  playlist: Playlist;
  page: number = globalConstants.pagination.defaultPage;
  private itemsPerPage: number;
  playedMusicForm: FormGroup;
  playMusic: Music;
  playlistId: string;
  isOwn: boolean = false;
  isOnInit: boolean = true;

  constructor(
    private modalService: NgbModal,
    private route: ActivatedRoute,
    private playlistService: PlaylistService,
    private playlistMusicsService: PlaylistMusicsService,
    private playedMusicService: PlayedMusicService,
    private formBuilder: FormBuilder,
    private musicService: MusicService,
    private authService: AuthService) {
    this.isAuth = authService.isAuth;
    this.authService.isAuthChanged.subscribe(() => {
      this.isAuth = this.authService.isAuth;
    })
    this.isOwn = false;
    this.itemsPerPage = globalConstants.pagination.itemsPerPage;
  }

  ngOnInit() {
    this.playedMusicForm = this.formBuilder.group({
      musicId: null,
    });
    this.playlist = this.route.snapshot.data.playlist;
    this.playlistId = this.playlist.id;
    this.getPlaylistsMusicsPerPage();
  }

  @HostListener("window:scroll", [])
  onWindowScroll() {
    //In chrome and some browser scroll is given to body tag
    var playlistSongsElement = document.getElementById("playlistSongs");

    let pos = (playlistSongsElement.scrollTop || playlistSongsElement.scrollTop) + playlistSongsElement.offsetHeight;
    let max = playlistSongsElement.scrollHeight;
    // pos/max will give you the distance between scroll bottom and and bottom of screen in percentage.
    if (pos > max * globalConstants.pagination.updateProcent) {
      //Do your action here
      this.getPlaylistsMusicsPerPage();
    }
  }

  onScrollPlaylistMusics() {
    //In chrome and some browser scroll is given to body tag
    var playlistSongsElement = document.getElementById("playlistMusics");

    let pos = (playlistSongsElement.scrollTop || playlistSongsElement.scrollTop) + playlistSongsElement.offsetHeight;
    let max = playlistSongsElement.scrollHeight;
    // pos/max will give you the distance between scroll bottom and and bottom of screen in percentage.
    if (pos > max * globalConstants.pagination.updateProcent) {
      //Do your action here
      this.getPlaylistsMusicsPerPage();
    }
  }

  private getPlaylistsMusicsPerPage(): void {

    var page = this.playlistMusicsPagination.currentPage + 1;
    if (page <= this.playlistMusicsPagination.numberOfPages) {

      this.playlistMusicsPagination.currentPage = page;
      this.playlistMusicsService.all(this.playlistId, page).subscribe(data => {
        this.playlistMusicsPagination.numberOfPages = data.numberOfPages;
        data.playlistMusics.forEach(playlistSong => {
          this.playlistMusicsPagination.playlistMusics.push(playlistSong);
        });
        if (this.isOnInit) {
          this.GetPlaylistData();
          this.isOnInit = false;
        }
      });
    }
  }

  private GetPlaylistData() {
    var playlistMusics = this.playlistMusicsPagination.playlistMusics;
    if (playlistMusics.length > 0) {
      var songId = playlistMusics[0].music.id;
      this.changePlayMusic(songId);
      if (this.isAuth == true) {
        this.playlistService.isOwn(this.playlist.id).subscribe(s => this.isOwn = s);
      }
    }
    else {
    }
  }

  changePlayMusic(musicId) {
    this.musicService.getById(musicId).subscribe(m => {
      this.playMusic = m;
      if (this.isAuth) {
        this.playedMusicForm.get('musicId').setValue(m.id);
        let playedMusic: PlayedMusic = this.playedMusicForm.value;
        this.playedMusicService.create(playedMusic).subscribe();
      }
      let player = <HTMLAudioElement>document.getElementById("player");
      player.src = "../../../../../assets/resources/song/аudio/" + this.playMusic.audioUrl;
      player.onended = () => {
        this.changeMusic();
      }
      player.play();
      
    });
  }

  changeMusic(): void {
    var indexOfPlayedMusic = 0;
    for (var i = 0; i < this.playlistMusicsPagination.playlistMusics.length; i++) {
      if (this.playlistMusicsPagination.playlistMusics[i].music.id == this.playMusic.id) {
        indexOfPlayedMusic = i + 1;
      }
    }

    if (indexOfPlayedMusic < this.playlistMusicsPagination.playlistMusics.length) {
      var musicId = this.playlistMusicsPagination.playlistMusics[indexOfPlayedMusic].music.id;
      this.changePlayMusic(musicId);
    }
  }
}
