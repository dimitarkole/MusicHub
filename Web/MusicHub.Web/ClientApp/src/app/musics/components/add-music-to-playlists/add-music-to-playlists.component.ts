import { Component, HostListener, Inject, Input, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
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
  selector: 'app-add-music-to-playlists',
  templateUrl: './add-music-to-playlists.component.html',
  styleUrls: ['./add-music-to-playlists.component.css']
})
export class AddMusicToPlaylistsComponent implements OnInit {
  @Input() musicId: string;
  playlistPagination: PlaylistPagination = { currentPage: 0, numberOfPages: 1, playlists: [] };
  searchForm: FormGroup;
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
    this.searchForm = this.formBuilder.group({
      name: [
        null,
      ],
      orderMethod: [
        "CreatedOnDesc"
      ]
    });

    this.getFilterPlaylistsPerPage();
  }

  @HostListener("window:scroll", [])
  onScrollPlaylists() {
    var playlistMusicsElement = document.getElementById("playlists");
    let pos = (playlistMusicsElement.scrollTop || playlistMusicsElement.scrollTop) + playlistMusicsElement.offsetHeight;
    let max = playlistMusicsElement.scrollHeight;
    // pos/max will give you the distance between scroll bottom and and bottom of screen in percentage.
    if (pos > max * globalConstants.pagination.updateProcent) {
      //Do your action here
      this.getFilterPlaylistsPerPage();
    }
  }

  submit() {
    let playlist: Playlist = this.createFilterModel();
    this.playlistPagination.currentPage = 1;
    this.playlistService.allFilterOwn(1, playlist).subscribe(data => {
      this.playlistPagination.numberOfPages = data.numberOfPages;
      this.playlistPagination.playlists = data.playlists;
    });
  }

  get name(): AbstractControl {
    return this.searchForm.get('name');
  }

  get orderMethod(): AbstractControl {
    return this.searchForm.get('orderMethod');
  }

  private getFilterPlaylistsPerPage(): void {
    var page = this.playlistPagination.currentPage + 1;
    if (page <= this.playlistPagination.numberOfPages) {
      this.playlistPagination.currentPage = page;
      let playlist: Playlist = this.createFilterModel();

      this.playlistService.allFilterOwn(page, playlist).subscribe(data => {
        this.playlistPagination.numberOfPages = data.numberOfPages;
        data.playlists.forEach(song => {
          this.playlistPagination.playlists.push(song);
        });
      });
    }
  }

  private createFilterModel() {
    let playlist: Playlist = this.searchForm.value;
    playlist.orderMethod = CreateOrderMethod(this.orderMethod.value);
    return playlist;
  }
}
