import { Component, HostListener, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { globalConstants } from '../../../common/global-constants';
import { CreateOrderMethod, OrderMethod } from '../../../shared/models/orderMethod';
import Playlist from '../../models/playlist';
import { PlaylistPagination } from '../../models/playlistPagination';
import { PlaylistService } from '../../services/playlist.service';

@Component({
  selector: 'app-playlists-list',
  templateUrl: './playlists-list.component.html',
  styleUrls: ['./playlists-list.component.css']
})
export class PlaylistsListComponent implements OnInit {
  playlistPagination: PlaylistPagination = { currentPage: 0, numberOfPages: 1, playlists: [] };
  isFilter: boolean = false;
  searchForm: FormGroup;

  constructor(private playlistService: PlaylistService,
    private formBuilder: FormBuilder) {
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
    this.getPlaylistsPerPage();
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
    playlist.orderMethod = CreateOrderMethod(this.orderMethod.value);
    return playlist;
  }
}
