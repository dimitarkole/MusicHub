import { Component, HostListener, Input, OnInit, SimpleChanges } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, NavigationEnd, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { globalConstants } from '../../../common/global-constants';
import { AuthService } from '../../../core/services/auth.service';
import { Music } from '../../../musics/models/music';
import { MusicPagination } from '../../../musics/models/musicPagination';
import { PlaylistPagination } from '../../../playlists/models/playlistPagination';
import { PlaylistService } from '../../../playlists/services/playlist.service';
import { MusicService } from '../../../musics/services/music.service';
import User from '../../../shared/models/user';
import { CreateOrderMethod } from '../../../shared/models/orderMethod';
import { FollowersListComponent } from '../../../follow/components/followers-list/followers-list.component';
import { MatDialog, MatDialogConfig } from '@angular/material';
import { FollowingListComponent } from '../../../follow/components/following-list/following-list.component';
import { ProfileEditComponent } from '../profile-edit/profile-edit.component';
import { ProfileChangePasswordComponent } from '../profile-change-password/profile-change-password.component';
import { ProfileService } from '../../services/profile.service';
import Playlist from '../../../playlists/models/playlist';

@Component({
  selector: 'app-profile-user',
  templateUrl: './profile-user.component.html',
  styleUrls: ['./profile-user.component.css']
})
export class ProfileUserComponent implements OnInit {
  isAuth: boolean = false;
  isMyProfile: boolean = false;
  musicPagination: MusicPagination = { currentPage: 0, numberOfPages: 1, musics: [] };
  playlistPagination: PlaylistPagination = { currentPage: 0, numberOfPages: 1, playlists: [] };

  defaultCategoryId: string = 'Each category';
  @Input() user: User;
  searchForm: FormGroup;

  constructor(
    private modalService: NgbModal,
    public matDialog: MatDialog,
    private playlistService: PlaylistService,
    private formBuilder: FormBuilder,
    private musicService: MusicService,
    private profileService: ProfileService,
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
      userId: "",
    });

    if (this.user == null) {
      this.profileService.MyProfile().subscribe(data => {
        this.user = data;
        this.user.id = data.id;
        this.userId.setValue(data.id);
        this.authService.isProfileMine(this.user.id).subscribe(data => {
          this.isMyProfile = data;
        });

        this.getMusicsPerPage();
        this.getPlaylistsPerPage();
      })
    }
    else {
      this.userId.setValue(this.user.id);
      this.authService.isProfileMine(this.user.id).subscribe(data => {
        this.isMyProfile = data;
      });

      this.getMusicsPerPage();
      this.getPlaylistsPerPage();
    }
  }

  openFollowing() {
    const dialogConfig = new MatDialogConfig();
    // The user can't close the dialog by clicking outside its body
    dialogConfig.id = "modal-component";
    dialogConfig.data = this.user.id;
    dialogConfig.width = "600px";
    // https://material.angular.io/components/dialog/overview
    const modalDialog = this.matDialog.open(FollowingListComponent, dialogConfig);
    modalDialog.afterClosed().subscribe(result => {
      if (result == true) window.location.reload();
    });
  }

  openFollowers() {
    const dialogConfig = new MatDialogConfig();
    // The user can't close the dialog by clicking outside its body
    dialogConfig.id = "modal-component";
    dialogConfig.data = this.user.id;
    dialogConfig.width = "600px";
    // https://material.angular.io/components/dialog/overview
    const modalDialog = this.matDialog.open(FollowersListComponent, dialogConfig);
    modalDialog.afterClosed().subscribe(result => {
      if (result == true) window.location.reload();
    });
  }

  openEdit() {
    const dialogConfig = new MatDialogConfig();
    // The user can't close the dialog by clicking outside its body
    dialogConfig.id = "modal-component";
    dialogConfig.width = "600px";
    // https://material.angular.io/components/dialog/overview
    const modalDialog = this.matDialog.open(ProfileEditComponent, dialogConfig);
    modalDialog.afterClosed().subscribe(result => {
      if (result == true) window.location.reload();
    });
  }

  openChangePassword() {
    let modal = this.modalService.open(ProfileChangePasswordComponent);

    modal.result.then(_ => {
      this.router.navigate(['/myProfile']);
    }).catch(err => {
      console.log(err);
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
      this.getMusicsPerPage();
      this.getPlaylistsPerPage();
    }
  }

  get name(): AbstractControl {
    return this.searchForm.get('name');
  }

  get userId(): AbstractControl {
    return this.searchForm.get('userId');
  }

  get categoryId(): AbstractControl {
    return this.searchForm.get('categoryId');
  }

  get orderMethod(): AbstractControl {
    return this.searchForm.get('orderMethod');
  }

  private getMusicsPerPage(): void {
    var page = this.musicPagination.currentPage + 1;
    if (page <= this.musicPagination.numberOfPages) {
      this.musicPagination.currentPage = page;
      let music: Music = this.createMusicFilterModel();
      this.musicService.allFilter(page, music).subscribe(data => {
        this.musicPagination.numberOfPages = data.numberOfPages;
        data.musics.forEach(Music => {
          this.musicPagination.musics.push(Music);
        });
      });
    }
  }

  private getPlaylistsPerPage(): void {
    var page = this.playlistPagination.currentPage + 1;
    if (page <= this.playlistPagination.numberOfPages) {
      this.playlistPagination.currentPage = page;
      let playlist: Playlist = this.createPlaylistFilterModel();
      this.playlistService.allFilter(page, playlist).subscribe(data => {
        this.playlistPagination.numberOfPages = data.numberOfPages;
        data.playlists.forEach(Music => {
          this.playlistPagination.playlists.push(Music);
        });
      });
    }
  }

  private createMusicFilterModel() {
    let music: Music = this.searchForm.value;
    music.userId = this.userId.value;
    music.orderMethod = CreateOrderMethod(this.orderMethod.value);
    if (music.categoryId == this.defaultCategoryId) {
      music.categoryId = null;
    }

    return music;
  }

  private createPlaylistFilterModel() {
    let playlist: Playlist = this.searchForm.value;
    playlist.userId = this.userId.value;
    playlist.orderMethod = CreateOrderMethod(this.orderMethod.value);
    return playlist;
  }
}
