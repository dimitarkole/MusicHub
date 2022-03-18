import { Component, Inject, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { ModalComponent } from 'ngb-modal';
import { globalConstants } from '../../../common/global-constants';
import { modelConstants } from '../../../common/model-constants';
import { MegaStorageFileService } from '../../../core/services/mega-storage-file.service';
import { CreatePrivacyStatus, PrivacyStatus, PrivacyStatusMapping } from '../../../shared/models/visibleStatus';
import Album from '../../models/album';
import { AlbumService } from '../../services/album.service';

@Component({
  selector: 'app-album-edit',
  templateUrl: './album-edit.component.html',
  styleUrls: ['./album-edit.component.css']
})
export class AlbumEditComponent implements OnInit {
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
    @Inject(MAT_DIALOG_DATA) public data: any,
    private albumService: AlbumService,
    private fileService: MegaStorageFileService,
    private formBuilder: FormBuilder,
  ) { }

  ngOnInit() {
    this.albumForm = this.formBuilder.group({
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
    let album: Album = this.albumForm.value;
    album.privacy = CreatePrivacyStatus(album.privacy);
    this.imageUrl.setValue(this.defaultAlbumImagePath);
    if (this.imageFileToUpload != null) {
      this.fileService.create(this.imageFileToUpload, globalConstants.music.imageFolderPath)
        .subscribe(data => {
          this.imageUrl.setValue(data["fileName"]);
          this.albumService.edit(album)
            .subscribe(_ => {
              this.dialogRef.close(); //It closes successfully
            })
        });
    }
    else {
      this.albumService.edit(album)
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
