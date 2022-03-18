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
import Album from '../../models/album';
import { AlbumMusicsPagination } from '../../models/albumtMusicsPagination';
import { AlbumMusicService } from '../../services/album-music.service';
import { AlbumService } from '../../services/album.service';

@Component({
  selector: 'app-album-play',
  templateUrl: './album-play.component.html',
  styleUrls: ['./album-play.component.css']
})
export class AlbumPlayComponent implements OnInit {
  albumMusicsPagination: AlbumMusicsPagination = { currentPage: 0, numberOfPages: 1, albumMusics: [] };
  isAuth: boolean = false;
  album: Album;
  page: number = globalConstants.pagination.defaultPage;
  private itemsPerPage: number;
  playedMusicForm: FormGroup;
  playMusic: Music;
  albumId: string;
  isOwn: boolean = false;
  isOnInit: boolean = true;

  constructor(
    private modalService: NgbModal,
    private route: ActivatedRoute,
    private albumService: AlbumService,
    private albumMusicsService: AlbumMusicService,
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
    this.album = this.route.snapshot.data.album;
    this.albumId = this.album.id;
    this.getAlbumsMusicsPerPage();
  }

  @HostListener("window:scroll", [])
  onWindowScroll() {
    //In chrome and some browser scroll is given to body tag
    var albumSongsElement = document.getElementById("albumSongs");

    let pos = (albumSongsElement.scrollTop || albumSongsElement.scrollTop) + albumSongsElement.offsetHeight;
    let max = albumSongsElement.scrollHeight;
    // pos/max will give you the distance between scroll bottom and and bottom of screen in percentage.
    if (pos > max * globalConstants.pagination.updateProcent) {
      //Do your action here
      this.getAlbumsMusicsPerPage();
    }
  }

  onScrollAlbumMusics() {
    //In chrome and some browser scroll is given to body tag
    var albumSongsElement = document.getElementById("albumMusics");

    let pos = (albumSongsElement.scrollTop || albumSongsElement.scrollTop) + albumSongsElement.offsetHeight;
    let max = albumSongsElement.scrollHeight;
    // pos/max will give you the distance between scroll bottom and and bottom of screen in percentage.
    if (pos > max * globalConstants.pagination.updateProcent) {
      //Do your action here
      this.getAlbumsMusicsPerPage();
    }
  }

  private getAlbumsMusicsPerPage(): void {

    var page = this.albumMusicsPagination.currentPage + 1;
    if (page <= this.albumMusicsPagination.numberOfPages) {

      this.albumMusicsPagination.currentPage = page;
      this.albumMusicsService.all(this.albumId, page).subscribe(data => {
        this.albumMusicsPagination.numberOfPages = data.numberOfPages;
        data.albumMusics.forEach(albumSong => {
          this.albumMusicsPagination.albumMusics.push(albumSong);
        });
        if (this.isOnInit) {
          this.GetAlbumData();
          this.isOnInit = false;
        }
      });
    }
  }

  private GetAlbumData() {
    var albumMusics = this.albumMusicsPagination.albumMusics;
    if (albumMusics.length > 0) {
      var songId = albumMusics[0].music.id;
      this.changePlayMusic(songId);
      if (this.isAuth == true) {
        this.albumService.isOwn(this.album.id).subscribe(s => this.isOwn = s);
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
    for (var i = 0; i < this.albumMusicsPagination.albumMusics.length; i++) {
      if (this.albumMusicsPagination.albumMusics[i].music.id == this.playMusic.id) {
        indexOfPlayedMusic = i + 1;
      }
    }

    if (indexOfPlayedMusic < this.albumMusicsPagination.albumMusics.length) {
      var musicId = this.albumMusicsPagination.albumMusics[indexOfPlayedMusic].music.id;
      this.changePlayMusic(musicId);
    }
  }
}
