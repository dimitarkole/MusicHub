import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material';
import { ModalComponent } from 'ngb-modal';
import { modelConstants } from '../../../common/model-constants';
//import { CreateVisibleStatus, VisibleStatus, VisibleStatusMapping } from '../../../shared/models/visibleStatus';
import Playlist from '../../models/playlist';
import { PlaylistService } from '../../services/playlist.service';

@Component({
  selector: 'app-playlist-create',
  templateUrl: './playlist-create.component.html',
  styleUrls: ['./playlist-create.component.css']
})
export class PlaylistCreateComponent implements OnInit {
  nameMinLength = modelConstants.playlist.nameMinLength;
  nameMaxLength = modelConstants.playlist.nameMaxLength;

 /* visibleStatusMapping = VisibleStatusMapping;
  visibleStatus = Object.keys(VisibleStatus)
    .filter(k => !isNaN(Number(k)));*/

  playlistForm: FormGroup

  constructor(public dialogRef: MatDialogRef<ModalComponent>,
    private playlistService: PlaylistService,
    private formBuilder: FormBuilder,
  ) { }

  ngOnInit() {
    this.playlistForm = this.formBuilder.group({
      name: [
        null,
        [
          Validators.required,
          Validators.minLength(this.nameMinLength),
          Validators.maxLength(this.nameMaxLength)
        ]
      ],
      imageUrl: [
        null,
        [
          Validators.required,
        ]
      ],
      visibleStatus: [
      //  VisibleStatus.Public,
        Validators.required
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
    //playlist.visibleStatus = CreateVisibleStatus(playlist.visibleStatus);

    this.playlistService.create(playlist)
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
