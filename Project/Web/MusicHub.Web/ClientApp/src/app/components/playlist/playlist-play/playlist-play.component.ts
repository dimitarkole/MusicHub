import { Component, OnInit, OnChanges, SimpleChanges, HostListener } from '@angular/core';
import { PlaylistSongDeleteComponent } from '../playlist-song-delete/playlist-song-delete.component';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Observable } from 'rxjs/internal/Observable';
import { FormBuilder, FormGroup } from '@angular/forms';
import { PlaylistAddSongComponent } from '../playlist-add-song/playlist-add-song.component';
import Playlist from '../../shared/models/playlist';
import { globalConstants } from '../../../common/global-constants';
import { Song } from '../../shared/models/song';
import PlaylistSong from '../../shared/models/playlistSong';
import { ActivatedRoute } from '@angular/router';
import { PlaylistService } from '../../../core/services/playlist.service';
import { SongService } from '../../../core/services/song.service';
import { LisenedSongService } from '../../../core/services/lisened-song.service';
import { AuthService } from '../../../core/services/auth.service';
import getPage from '../../../common/paginator';
import listenedSong from '../../shared/models/listenedSong';
import { PlaylistSongsService } from '../../../core/services/playlist-songs.service';
import { PlaylistSongsPagination } from '../../shared/models/playlistsongsPagination';

@Component({
  selector: 'app-playlist-play',
  templateUrl: './playlist-play.component.html',
  styleUrls: ['./playlist-play.component.css']
})
export class PlaylistPlayComponent implements OnInit {
  playlistSongsPagination: PlaylistSongsPagination = { currentPage: 0, numberOfPages: 1, playlistSongs: [] };
  isAuth: boolean = false;
  playlist: Playlist;
  page: number = globalConstants.pagination.defaultPage;
  private itemsPerPage: number;

  playSong: Song;
  allPlaylistSongs: PlaylistSong[] = [];
  playlistSongs: PlaylistSong[] = [];
  playlistId: string;
  isOwn: boolean = false;
  lisenedSongForm: FormGroup;
  isOnInit: boolean = true;

  constructor(
    private modalService: NgbModal,
    private route: ActivatedRoute,
    private playlistService: PlaylistService,
    private playlistSongsService: PlaylistSongsService,
    private formBuilder: FormBuilder,
    private songService: SongService,
    private lisenedSongService: LisenedSongService,
    private authService: AuthService) {
    this.isAuth = authService.isAuth;
    this.authService.isAuthChanged.subscribe(() => {
      this.isAuth = this.authService.isAuth;
    })
    this.isOwn = false;
    this.itemsPerPage = globalConstants.pagination.itemsPerPage;
  }

  ngOnInit() {
    this.playlist = this.route.snapshot.data.playlist;
    this.playlistId = this.playlist.id;
    this.getPlaylistsSongsPerPage();
  }

  changePlaySong(songId) {
    this.songService.getById(songId).subscribe(s => {
      this.playSong = s;
      let player = <HTMLAudioElement>document.getElementById("player");
      player.src = "../../../../../assets/resources/song/аudio/" + this.playSong.audioFilePath;
      player.onended = () => {
        this.changeSong();
      }
      player.play();

      this.addSongToLisenedHistory();
    });
  }

  changeSong(): void {
    var songCountOfPlaylist = 0;
    for (var i = 0; i < this.allPlaylistSongs.length; i++) {
      if (this.allPlaylistSongs[i].song.id == this.playSong.id) {
        songCountOfPlaylist = i + 1;
      }
    }

    if (songCountOfPlaylist < this.allPlaylistSongs.length) {
      var songId = this.allPlaylistSongs[songCountOfPlaylist].song.id;
      this.changePlaySong(songId);
    }
  }

  openDeleteSong(playlistSongId: string) {
    let modal = this.modalService.open(PlaylistSongDeleteComponent);
    modal.result.then(value => {
      this.playlistSongsService.delete(playlistSongId).toPromise()
        .then(_ => this.GetPlaylistData())
    }).catch(err => {
      console.log(err);
    })
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
      this.getPlaylistsSongsPerPage();
    }
  }

  onScrollPlaylistSongs() {
    //In chrome and some browser scroll is given to body tag
    var playlistSongsElement = document.getElementById("playlistSongs");

    let pos = (playlistSongsElement.scrollTop || playlistSongsElement.scrollTop) + playlistSongsElement.offsetHeight;
    let max = playlistSongsElement.scrollHeight;
    // pos/max will give you the distance between scroll bottom and and bottom of screen in percentage.
    if (pos > max * globalConstants.pagination.updateProcent) {
      //Do your action here
      this.getPlaylistsSongsPerPage();
    }
  }

  private getPlaylistsSongsPerPage(): void {
    var page = this.playlistSongsPagination.currentPage + 1;
    if (page <= this.playlistSongsPagination.numberOfPages) {
      this.playlistSongsPagination.currentPage = page;
      this.playlistSongsService.all(this.playlistId, page).subscribe(data => {
        this.playlistSongsPagination.numberOfPages = data.numberOfPages;
        data.playlistSongs.forEach(playlistSong => {
          this.playlistSongsPagination.playlistSongs.push(playlistSong);
        });
        if (this.isOnInit) {
          this.GetPlaylistData();
          this.isOnInit = false;
        }
      });
    }
  }

  private GetPlaylistData() {
    var playlistSongs = this.playlistSongsPagination.playlistSongs;
    if (playlistSongs.length > 0) {
      var songId = playlistSongs[0].song.id;
      this.changePlaySong(songId);
      if (this.isAuth == true) {
        this.playlistService.isOwn(this.playlist.id).subscribe(s => this.isOwn = s);
      }
    } 
    else {
    }
  }

  public addSongToLisenedHistory(): void {
    if (this.isAuth) {
      this.lisenedSongForm = this.formBuilder.group({
        songId: null,
      });
      if (this.isAuth) {
        this.lisenedSongForm.get('songId').setValue(this.playSong.id);
        let lisenedSong: listenedSong = this.lisenedSongForm.value;
        this.lisenedSongService.create(lisenedSong)
          .subscribe(_ => _);
      } 
    } 
  }

  openAddToSong() {
    let modal = this.modalService.open(PlaylistAddSongComponent);
    modal.componentInstance.song = this.playSong;
    modal.result.then(_ => {
    }).catch(err => {
      console.log(err);
    })
  }
}
