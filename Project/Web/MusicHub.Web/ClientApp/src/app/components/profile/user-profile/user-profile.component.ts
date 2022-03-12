import { Component, OnInit, Input, HostListener, OnChanges, SimpleChanges } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { AbstractControl, FormBuilder, FormGroup } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';
import User from '../../shared/models/user';
import { FollowingListComponent } from '../../follow/following-list/following-list.component';
import { FollowersListComponent } from '../../follow/followers-list/followers-list.component';
import { globalConstants } from '../../../common/global-constants';
import { Song } from '../../shared/models/song';
import { OrderMethod } from '../../shared/models/OrderMethod';
import { SongPagination } from '../../shared/models/songPagination';
import { SongService } from '../../../core/services/song.service';
import { PlaylistPagination } from '../../shared/models/playlistPagination';
import { PlaylistService } from '../../../core/services/playlist.service';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit, OnChanges {
  isAuth: boolean = false;
  songPagination: SongPagination = { currentPage: 0, numberOfPages: 2, songs: [] };
  playlistPagination: PlaylistPagination = { currentPage: 0, numberOfPages: 1, playlists: [] };

  defaultCategoryId: string = 'Each category';
  @Input() user: User;
  searchForm: FormGroup;

  constructor(
    private modalService: NgbModal,
    private playlistService: PlaylistService,
    private formBuilder: FormBuilder,
    private songService: SongService,
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

  ngOnInit(): void {
    this.preparedPage();
  }

  private preparedPage() {
    this.user = this.route.snapshot.data.user;
    console.log(this.user);
        this.searchForm = this.formBuilder.group({
            name: [
                null,
            ],
            categoryId: [
                this.defaultCategoryId
            ],
            orderMethod: [
                "CreatedOnDesc"
            ],
            userId: this.user.id,
        });
        this.getSongsPerPage();
        this.getPlaylistsPerPage();
    }

  openFollowing() {
    let modal = this.modalService.open(FollowingListComponent);
    modal.componentInstance.userId = this.user.id;
    modal.result.then(_ => {
    })
  }

  openFollowers() {
    let modal = this.modalService.open(FollowersListComponent);
    modal.componentInstance.userId = this.user.id;
    modal.result.then(_ => {
    })
  }

  @HostListener("window:scroll", [])
  onWindowScroll() {
    //In chrome and some browser scroll is given to body tag
    let pos = (document.documentElement.scrollTop || document.body.scrollTop) + document.documentElement.offsetHeight;
    let max = document.documentElement.scrollHeight;
    // pos/max will give you the distance between scroll bottom and and bottom of screen in percentage.
    if (pos > max * globalConstants.pagination.updateProcent) {
      //Do your action here
      this.getSongsPerPage();
      this.getPlaylistsPerPage();
    }
  }

  get name(): AbstractControl {
    return this.searchForm.get('name');
  }

  get categoryId(): AbstractControl {
    return this.searchForm.get('categoryId');
  }

  get orderMethod(): AbstractControl {
    return this.searchForm.get('orderMethod');
  }

  private getSongsPerPage(): void {
    var page = this.songPagination.currentPage + 1;
    if (page <= this.songPagination.numberOfPages) {
      this.songPagination.currentPage = page;
      let song: Song = this.createFilterModel();
      this.songService.allFilter(page, song).subscribe(data => {
        this.songPagination.numberOfPages = data.numberOfPages;
        data.songs.forEach(song => {
          this.songPagination.songs.push(song);
        });
      });
    }
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
        console.log(data);
      });
    }
  }

  private createFilterModel() {
    let song: Song = this.searchForm.value;

    switch (this.orderMethod.value) {
      case "CreatedOnDesc": {
        song.orderMethod = OrderMethod.CreatedOnDesc;
        break;
      }
      case "NameAsc": {
        song.orderMethod = OrderMethod.NameAsc;
        break;
      }
      case "NameDesc": {
        song.orderMethod = OrderMethod.NameDesc;
        break;
      }
      default: {
        song.orderMethod = OrderMethod.CreatedOnAsc;
        break;
      }
    }

    if (song.categoryId == this.defaultCategoryId) {
      song.categoryId = null;
    }

    return song;
  }
}
