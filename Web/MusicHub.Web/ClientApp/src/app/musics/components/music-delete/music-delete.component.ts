import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { ModalComponent } from 'ngb-modal';
import { MusicService } from '../../services/music.service';

@Component({
  selector: 'app-music-delete',
  templateUrl: './music-delete.component.html',
  styleUrls: ['./music-delete.component.css']
})
export class MusicDeleteComponent {
  constructor(public dialogRef: MatDialogRef<ModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private musicService: MusicService) {
  }

  // If the user clicks the cancel button a.k.a. the go back button, then\
  // just close the modal
  closeModal() {
    this.dialogRef.close();
  }

  deleteMusic() {
    this.musicService.delete(this.data).subscribe(_ => {
      // TODO: delete music files - audio and image

      /*
       *
           this.songService.getById(songId).subscribe(songData => {
           this.deleteForm.get('fileName').setValue(songData.audioFilePath);
        this.deleteForm.get('path').setValue(globalConstants.songAudioPath);
        let audioFile: FileInfo = this.deleteForm.value;

        this.fileService.deleteFile(audioFile).subscribe(_ => {
          if (songData.imageFilePath != globalConstants.defaultImage) {
            this.deleteForm.get('fileName').setValue(songData.imageFilePath);
            this.deleteForm.get('path').setValue(globalConstants.songImagePath);
            let imageFile: FileInfo = this.deleteForm.value;
            this.fileService.deleteFile(imageFile).subscribe(_ => {
              this.deleteSong(songId);
            });
          }
          else {
            this.deleteForm.get('path').setValue(globalConstants.songImagePath);
            let imageFile: FileInfo = this.deleteForm.value;
            this.fileService.deleteFile(imageFile).subscribe(_ => {
              this.deleteSong(songId);
            });
          }
        });
       * */
      this.dialogRef.close();
    });
  }
}
