import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material';
import { ModalComponent } from 'ngb-modal';
import { globalConstants } from '../../../common/global-constants';
import { modelConstants } from '../../../common/model-constants';
import { MegaStorageFileService } from '../../../core/services/mega-storage-file.service';
import { CreatePrivacyStatus, PrivacyStatus, PrivacyStatusMapping } from '../../../shared/models/visibleStatus';
import Album from '../../models/album';
import { AlbumService } from '../../services/album.service';

@Component({
  selector: 'app-album-create',
  templateUrl: './album-create.component.html',
  styleUrls: ['./album-create.component.css']
})
export class AlbumCreateComponent implements OnInit {
  nameMinLength = modelConstants.album.nameMinLength;
  nameMaxLength = modelConstants.album.nameMaxLength;
  albumFolderPath: string = globalConstants.playlist.imageFolderPathToUpload;
  defaultAlbumImagePath: string = this.albumFolderPath + globalConstants.playlist.defauldImage;
  imageFileToUpload: File = null;
  privacyStatusMapping = PrivacyStatusMapping;
  privacyStatus = Object.keys(PrivacyStatus)
    .filter(k => !isNaN(Number(k)));;
  albumForm: FormGroup;

  constructor(public dialogRef: MatDialogRef<ModalComponent>,
    private albumService: AlbumService,
    private fileService: MegaStorageFileService,
    private formBuilder: FormBuilder,
  ) { }

  ngOnInit() {
    this.albumForm = this.formBuilder.group({
      name: [
        null,
        [
          Validators.required,
          Validators.minLength(this.nameMinLength),
          Validators.maxLength(this.nameMaxLength)
        ]
      ],
      imageUrl: [
        this.defaultAlbumImagePath,
        [
          // Validators.required,
        ]
      ],
      privacy: [
        PrivacyStatus.Public,
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
    let album: Album = this.albumForm.value;
    //playlist.visibleStatus = CreateVisibleStatus(playlist.visibleStatus);
    album.privacy = CreatePrivacyStatus(album.privacy);
    this.imageUrl.setValue(this.defaultAlbumImagePath);
    if (this.imageFileToUpload != null) {
      this.fileService.create(this.imageFileToUpload, globalConstants.music.imageFolderPath)
        .subscribe(data => {
          this.imageUrl.setValue(data["fileName"]);
          this.albumService.create(album)
            .subscribe(_ => {
              this.dialogRef.close(); //It closes successfully
            })
        });
    }
    else {
      this.albumService.create(album)
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
    this.imageUrl.setValue(this.defaultAlbumImagePath);
  }

  get name(): AbstractControl {
    return this.albumForm.get('name');
  }

  get privacy(): AbstractControl {
    return this.albumForm.get('privacy');
  }

  get imageUrl(): AbstractControl {
    return this.albumForm.get('imageUrl');
  }
}
