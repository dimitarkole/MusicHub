import { Component, HostListener, Input, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup } from '@angular/forms';
import { MatDialog, MatDialogConfig } from '@angular/material';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { globalConstants } from '../../../common/global-constants';
import { AuthService } from '../../../core/services/auth.service';
import { OrderMethod } from '../../../shared/models/orderMethod';
import Playlist from '../../models/playlist';
import { PlaylistPagination } from '../../models/playlistPagination';
import { PlaylistService } from '../../services/playlist.service';
import { PlaylistDeleteComponent } from '../playlist-delete/playlist-delete.component';
import { PlaylistEditComponent } from '../playlist-edit/playlist-edit.component';

@Component({
  selector: 'app-playlists-list-template',
  templateUrl: './playlists-list-template.component.html',
  styleUrls: ['./playlists-list-template.component.css']
})
export class PlaylistsListTemplateComponent implements OnInit {
  @Input() playlists: Playlist[] = [];
  @Input() type: string;
  playlistDefaultImagePath: string = globalConstants.playlist.imageFolderPathToUpload + globalConstants.playlist.defauldImage;

  constructor(
    public matDialog: MatDialog,
    private router: Router,
    private authService: AuthService) { }

  ngOnInit() {
  }

  openEdit(playlist: Playlist) {
    const dialogConfig = new MatDialogConfig();
    // The user can't close the dialog by clicking outside its body
    dialogConfig.id = "modal-component";
    dialogConfig.width = "600px";
    dialogConfig.data = playlist;
    // https://material.angular.io/components/dialog/overview
    const modalDialog = this.matDialog.open(PlaylistEditComponent, dialogConfig);
    modalDialog.afterClosed().subscribe(result => {
      if (result == true) window.location.reload();
    });
  }

  openDelete(playlistId: string) {
    const dialogConfig = new MatDialogConfig();
    // The user can't close the dialog by clicking outside its body
    dialogConfig.id = "modal-component";
    dialogConfig.width = "600px";
    dialogConfig.data = playlistId;
    // https://material.angular.io/components/dialog/overview
    const modalDialog = this.matDialog.open(PlaylistDeleteComponent, dialogConfig);
    modalDialog.afterClosed().subscribe(result => {
      if (result == true) window.location.reload();
    });
  }
}
