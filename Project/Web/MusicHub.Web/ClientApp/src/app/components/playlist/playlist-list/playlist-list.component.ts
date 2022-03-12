import { Component, OnInit, Input, OnChanges, SimpleChanges, HostListener } from '@angular/core';
import { Router } from '@angular/router';
import { PlaylistCreateComponent } from '../playlist-create/playlist-create.component';
import { PlaylistEditComponent } from '../playlist-edit/playlist-edit.component';
import { PlaylistDeleteComponent } from '../playlist-delete/playlist-delete.component';
import { FormBuilder, FormGroup, AbstractControl } from '@angular/forms';
import { globalConstants } from '../../../common/global-constants';
import Playlist from '../../shared/models/playlist';
import { PlaylistService } from '../../../core/services/playlist.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { AuthService } from '../../../core/services/auth.service';
import { OrderMethod } from '../../shared/models/OrderMethod';
import getPage from '../../../common/paginator';
import { Observable } from 'rxjs';
import Category from '../../shared/models/category';
import { PlaylistPagination } from '../../shared/models/playlistPagination';

@Component({
  selector: 'app-playlist-list',
  templateUrl: './playlist-list.component.html',
  styleUrls: ['./playlist-list.component.css']
})
export class PlaylistListComponent implements OnInit {
  playlistPagination: PlaylistPagination = { currentPage: 0, numberOfPages: 1, playlists: [] };
  isFilter: boolean = false;
  searchForm: FormGroup;

  constructor(private modalService: NgbModal,
    private playlistService: PlaylistService,
    private formBuilder: FormBuilder,
    private router: Router,
    private authService: AuthService) {
    if (authService.isAuth == false) {
      this.router.navigate(['']);
    }  
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

  openCreate() {
    let modal = this.modalService.open(PlaylistCreateComponent);

    modal.result.then(_ => {
      this.router.navigate(['/playlist/own']);
    }).catch(err => {
      console.log(err);
    })
  }

  private getPlaylistsPerPage(): void {
    var page = this.playlistPagination.currentPage + 1;
    if (page <= this.playlistPagination.numberOfPages) {
      this.playlistPagination.currentPage = page;
      this.playlistService.allOwn(page).subscribe(data => {
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
