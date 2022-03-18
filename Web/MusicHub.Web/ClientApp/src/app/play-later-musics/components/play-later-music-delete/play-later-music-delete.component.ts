import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { ModalComponent } from 'ngb-modal';
import { PlayLaterMusicService } from '../../services/play-later-music.service';

@Component({
  selector: 'app-play-later-music-delete',
  templateUrl: './play-later-music-delete.component.html',
  styleUrls: ['./play-later-music-delete.component.css']
})
export class PlayLaterMusicDeleteComponent {
  constructor(public dialogRef: MatDialogRef<ModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private playLatermusicService: PlayLaterMusicService) {
  }

  // If the user clicks the cancel button a.k.a. the go back button, then\
  // just close the modal
  closeModal() {
    this.dialogRef.close();
  }

  deletePlayLaterMusic() {
    this.playLatermusicService.delete(this.data).subscribe(_ => {
      this.dialogRef.close();
    });
  }
}
