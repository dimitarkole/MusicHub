import { Component, HostListener, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { FormBuilder, FormGroup, AbstractControl } from '@angular/forms';
import { PlaylistSongDeleteComponent } from '../playlist-song-delete/playlist-song-delete.component';
import { globalConstants } from '../../../common/global-constants';
import Playlist from '../../shared/models/playlist';
import { PlaylistService } from '../../../core/services/playlist.service';
import { OrderMethod } from '../../shared/models/OrderMethod';
import getPage from '../../../common/paginator';
import { PlaylistPagination } from '../../shared/models/playlistPagination';
import { Router } from '@angular/router';

@Component({
  selector: 'app-playlist-list-all',
  templateUrl: './playlist-list-all.component.html',
  styleUrls: ['./playlist-list-all.component.css']
})

export class PlaylistListAllComponent implements OnInit {
  playlistPagination: PlaylistPagination = { currentPage: 0, numberOfPages: 1, playlists: [] };
  isFilter: boolean = false;
  searchForm: FormGroup;

  constructor(private modalService: NgbModal,
    private playlistService: PlaylistService,
    private formBuilder: FormBuilder,
    private router: Router) {
  }

  ngOnInit(): void {
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
  onWindowScroll() {
    //In chrome and some browser scroll is given to body tag
    let pos = (document.documentElement.scrollTop || document.body.scrollTop) + document.documentElement.offsetHeight;
    let max = document.documentElement.scrollHeight;
    // pos/max will give you the distance between scroll bottom and and bottom of screen in percentage.
    if (pos > max * globalConstants.pagination.updateProcent) {
      //Do your action here
      if (this.isFilter == false) {
        this.getPlaylistsPerPage();
      }
      else {
        this.getFilterPlaylistsPerPage();
      }
    }
  }

  submit() {
    let playlist: Playlist = this.createFilterModel();
    this.isFilter = true;
    this.playlistPagination.currentPage = 1;
    this.playlistService.allFilter(1, playlist).subscribe(data => {
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

  private getPlaylistsPerPage(): void {
    var page = this.playlistPagination.currentPage + 1;

    if (page <= this.playlistPagination.numberOfPages) {
      this.playlistPagination.currentPage = page;
      this.playlistService.get(page).subscribe(data => {
        this.playlistPagination.numberOfPages = data.numberOfPages;
        data.playlists.forEach(song => {
          this.playlistPagination.playlists.push(song);
        });
        console.log(data);
      });
    }
  }

  private getFilterPlaylistsPerPage(): void {
    var page = this.playlistPagination.currentPage + 1;
    if (page <= this.playlistPagination.numberOfPages) {
      this.playlistPagination.currentPage = page;
      let playlist: Playlist = this.createFilterModel();

      this.playlistService.allFilter(page, playlist).subscribe(data => {
        this.playlistPagination.numberOfPages = data.numberOfPages;
        data.playlists.forEach(song => {
          this.playlistPagination.playlists.push(song);
        });
      });
    }
  }

  private createFilterModel() {
    let playlist: Playlist = this.searchForm.value;
    switch (this.orderMethod.value) {
      case "CreatedOnDesc": {
        playlist.orderMethod = OrderMethod.CreatedOnDesc;
        break;
      }
      case "NameAsc": {
        playlist.orderMethod = OrderMethod.NameAsc;
        break;
      }
      case "NameDesc": {
        playlist.orderMethod = OrderMethod.NameDesc;
        break;
      }
      default: {
        playlist.orderMethod = OrderMethod.CreatedOnAsc;
        break;
      }
    }

    return playlist;
  }
}
