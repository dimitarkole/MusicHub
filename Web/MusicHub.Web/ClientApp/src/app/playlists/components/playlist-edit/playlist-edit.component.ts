import { Component, Inject, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { ModalComponent } from 'ngb-modal';
import { modelConstants } from '../../../common/model-constants';
import { VisibleStatus, VisibleStatusMapping } from '../../../shared/models/visibleStatus';
import Playlist from '../../models/playlist';
import { PlaylistService } from '../../services/playlist.service';

@Component({
  selector: 'app-playlist-edit',
  templateUrl: './playlist-edit.component.html',
  styleUrls: ['./playlist-edit.component.css']
})
export class PlaylistEditComponent implements OnInit {
  nameMinLength = modelConstants.playlist.nameMinLength;
  nameMaxLength = modelConstants.playlist.nameMaxLength;
  visibleStatusMapping = VisibleStatusMapping;
  visibleStatus = Object.keys(VisibleStatus)
    .filter(k => !isNaN(Number(k)));;

  playlistForm: FormGroup

  constructor(public dialogRef: MatDialogRef<ModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private playlistService: PlaylistService,
    private formBuilder: FormBuilder,
  ) { }

  ngOnInit() {
    this.playlistForm = this.formBuilder.group({
      id: this.data.id,
      name: [
        this.data.name,
        [
          Validators.required,
          Validators.minLength(this.nameMinLength),
          Validators.maxLength(this.nameMaxLength)
        ]
      ],
      imageUrl: [
        this.data.ImageUrl,
        [
          Validators.required,
        ]
      ],
      visibleStatus: [
        this.data.visibleStatus,
        [
          Validators.required
        ]
      ],
    })
  }

  // If the user clicks the cancel button a.k.a. the go back button, then\
  // just close the modal
  closeModal() {
    this.dialogRef.close();
  }

  OnSubmit() {
    let playlist: Playlist = this.playlistForm.value;
    if (playlist.visibleStatus == 0) {
      playlist.visibleStatus = VisibleStatus.Public;
    }
    else if (playlist.visibleStatus == 1) {
      playlist.visibleStatus = VisibleStatus.Hidden;
    }
    else {
      playlist.visibleStatus = VisibleStatus.OnlyMe;
    }

    this.playlistService.edit(playlist)
      .subscribe(_ => {
        this.dialogRef.close(); //It closes successfully
      })
  }

  get name(): AbstractControl {
    return this.playlistForm.get('name');
  }

  get visibleStatusForm(): AbstractControl {
    return this.playlistForm.get('visibleStatus');
  }

  get imageUrl(): AbstractControl {
    return this.playlistForm.get('imageUrl');
  }
}
