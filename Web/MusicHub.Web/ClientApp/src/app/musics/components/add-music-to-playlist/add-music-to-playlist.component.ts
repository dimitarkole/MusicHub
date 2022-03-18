import { Component, HostListener, Inject, Input, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { ModalComponent } from 'ngb-modal';
import { Observable } from 'rxjs';
import { globalConstants } from '../../../common/global-constants';
import { AuthService } from '../../../core/services/auth.service';
import Playlist from '../../../playlists/models/playlist';
import PlaylistMusic from '../../../playlists/models/playlistMusic';
import { PlaylistPagination } from '../../../playlists/models/playlistPagination';
import { PlaylistMusicsService } from '../../../playlists/services/playlist-musics.service';
import { PlaylistService } from '../../../playlists/services/playlist.service';
import { CreateOrderMethod } from '../../../shared/models/orderMethod';

@Component({
  selector: 'app-add-music-to-playlist',
  templateUrl: './add-music-to-playlist.component.html',
  styleUrls: ['./add-music-to-playlist.component.css']
})

export class AddMusicToPlaylistComponent implements OnInit {
  @Input() playlist: Playlist;
  @Input() musicId: string;
  playlistMusicForm: FormGroup
  isChecked: boolean = false;
  form: FormGroup;
  isSuccess: boolean = false;

  public progress: number;
  public message: string;
  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    public modal: NgbActiveModal,
    public playlistMusicsService: PlaylistMusicsService,
    public playlistService: PlaylistService,
    private authService: AuthService) {
    if (authService.isAuth == false) {
      this.router.navigate(['']);
    }
  }

  ngOnInit() {
    this.playlistMusicForm = this.formBuilder.group({
      playlistId: [
        this.playlist.id,
        [
          Validators.required,
          Validators.pattern('^((?!default).)*$'),
        ]
      ],
      musicId: this.musicId,
    });

    var playlistMusic: PlaylistMusic = { playlistId: this.playlist.id, musicId: this.musicId };
    this.playlistMusicsService.isAddedToPlaylist(playlistMusic).subscribe(r => this.isChecked=r);
  }

  checkValue(event: any) {
    if (event.currentTarget.checked) {
      // add music to playlist
      let addMusicToPlaylist: PlaylistMusic = this.playlistMusicForm.value;
      this.playlistMusicsService.create(addMusicToPlaylist)
        .subscribe(_ => this.successfull());
    }
    else {
      // remove music from playlist
      let addMusicToPlaylist: PlaylistMusic = this.playlistMusicForm.value;
      this.playlistMusicsService.getPlaylistMusicId(addMusicToPlaylist)
        .subscribe(r => {
          this.playlistMusicsService.delete(r.id).subscribe(_ => _);
          this.successfull();
        })
    }
  }

  successfull() {
    this.isSuccess = true;
    setTimeout(() => { this.isSuccess = false}, 5000);
  }

  wait(ms) {
    var start = new Date().getTime();
    var end = start;
    while (end < start + ms) {
      end = new Date().getTime();
    }
  }

  get playlistId(): AbstractControl {
    return this.playlistMusicForm.get('playlistId');
  }
}
