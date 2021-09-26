import { Component, Inject, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { ModalComponent } from 'ngb-modal';
import { globalConstants } from '../../../common/global-constants';
import { modelConstants } from '../../../common/model-constants';
import { MegaStorageFileService } from '../../../core/services/mega-storage-file.service';
import { CreatePrivacyStatus, PrivacyStatus, PrivacyStatusMapping } from '../../../shared/models/visibleStatus';
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
  playlistFolderPath: string = globalConstants.playlist.imageFolderPathToUpload;
  defaultPlaylistImagePath: string = this.playlistFolderPath + globalConstants.playlist.defauldImage;
  imageFileToUpload: File = null;privacyStatusMapping = PrivacyStatusMapping;
  privacyStatus = Object.keys(PrivacyStatus)
    .filter(k => !isNaN(Number(k)));;

  playlistForm: FormGroup

  constructor(public dialogRef: MatDialogRef<ModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private playlistService: PlaylistService,
    private fileService: MegaStorageFileService,
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
      privacy: [
        this.data.privacy,
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
    playlist.privacy = CreatePrivacyStatus(playlist.privacy);
    this.imageUrl.setValue(this.defaultPlaylistImagePath);
    if (this.imageFileToUpload != null) {
      this.fileService.create(this.imageFileToUpload, globalConstants.music.imageFolderPath)
        .subscribe(data => {
          this.imageUrl.setValue(data["fileName"]);
          this.playlistService.edit(playlist)
            .subscribe(_ => {
              this.dialogRef.close(); //It closes successfully
            })
        });
    }
    else {
      this.playlistService.edit(playlist)
        .subscribe(_ => {
          this.dialogRef.close(); //It closes successfully
        })
    }
  }

  handleImageFileInput(file: FileList) {
    this.imageFileToUpload = file.item(0);
    //Show image preview
    var reader = new FileReader();
    reader.onload = (event: any) => {
      this.imageUrl.setValue(event.target.result);
    }
    reader.readAsDataURL(this.imageFileToUpload);
  }

  useDefaultPicture() {
    this.imageUrl.setValue(this.defaultPlaylistImagePath);
  }

  get name(): AbstractControl {
    return this.playlistForm.get('name');
  }

  get privacy(): AbstractControl {
    return this.playlistForm.get('privacy');
  }

  get imageUrl(): AbstractControl {
    return this.playlistForm.get('imageUrl');
  }
}
