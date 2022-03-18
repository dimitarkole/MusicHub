import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { ModalComponent } from 'ngb-modal';
import { AlbumService } from '../../services/album.service';

@Component({
  selector: 'app-album-delete',
  templateUrl: './album-delete.component.html',
  styleUrls: ['./album-delete.component.css']
})

export class AlbumDeleteComponent {
  constructor(public dialogRef: MatDialogRef<ModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private albumService: AlbumService) {
  }

  // If the user clicks the cancel button a.k.a. the go back button, then\
  // just close the modal
  closeModal() {
    this.dialogRef.close();
  }

  delete() {
    this.albumService.delete(this.data).subscribe(_ => {
      this.dialogRef.close();
    });
  }
}
