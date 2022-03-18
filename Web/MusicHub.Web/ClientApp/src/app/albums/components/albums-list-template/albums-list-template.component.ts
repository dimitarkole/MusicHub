import { Component, Input, OnInit } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material';
import { Router } from '@angular/router';
import { globalConstants } from '../../../common/global-constants';
import { AuthService } from '../../../core/services/auth.service';
import Album from '../../models/album';
import { AlbumDeleteComponent } from '../album-delete/album-delete.component';
import { AlbumEditComponent } from '../album-edit/album-edit.component';

@Component({
  selector: 'app-albums-list-template',
  templateUrl: './albums-list-template.component.html',
  styleUrls: ['./albums-list-template.component.css']
})
export class AlbumsListTemplateComponent implements OnInit {
  @Input() albums: Album[] = [];
  @Input() type: string;
  albumDefaultImagePath: string = globalConstants.album.imageFolderPathToUpload + globalConstants.album.defauldImage;

  constructor(
    public matDialog: MatDialog,
    private router: Router,
    private authService: AuthService) { }

  ngOnInit() {
  }

  openEdit(album: Album) {
    const dialogConfig = new MatDialogConfig();
    // The user can't close the dialog by clicking outside its body
    dialogConfig.id = "modal-component";
    dialogConfig.width = "600px";
    dialogConfig.data = album;
    // https://material.angular.io/components/dialog/overview
    const modalDialog = this.matDialog.open(AlbumEditComponent, dialogConfig);
    modalDialog.afterClosed().subscribe(result => {
      if (result == true) window.location.reload();
    });
  }

  openDelete(albumId: string) {
    const dialogConfig = new MatDialogConfig();
    // The user can't close the dialog by clicking outside its body
    dialogConfig.id = "modal-component";
    dialogConfig.width = "600px";
    dialogConfig.data = albumId;
    // https://material.angular.io/components/dialog/overview
    const modalDialog = this.matDialog.open(AlbumDeleteComponent, dialogConfig);
    modalDialog.afterClosed().subscribe(result => {
      if (result == true) window.location.reload();
    });
  }
}
