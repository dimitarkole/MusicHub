import { Component, OnInit, Output, EventEmitter, Input, Type } from '@angular/core';
import { Observable } from 'rxjs';
import { FormGroup, FormBuilder, Validators, AbstractControl } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { HttpEventType } from '@angular/common/http';
import Category from '../../shared/models/category';
import { CategoryService } from '../../../core/services/category.service';
import { SongService } from '../../../core/services/song.service';
import { AuthService } from '../../../core/services/auth.service';
import { Song } from '../../shared/models/song';
import { FileService } from '../../../core/services/file.service';
import { globalConstants } from '../../../common/global-constants';
import FileInfo from '../../shared/models/file';
import { MusicLicenseType, MusicLicenseTypeMapping } from '../../shared/models/musicLicenseType';
import { VisibleStatusMapping, VisibleStatus } from '../../shared/models/visibleStatus';
import License from '../../shared/models/license';
import MusicLicense from '../../shared/models/musicLicense';
import { MusicLicenseServiceService } from '../../../core/services/music-license-service.service';
import { LicenseService } from '../../../core/services/license.service';

@Component({
  selector: 'app-song-edit',
  templateUrl: './song-edit.component.html',
  styleUrls: ['./song-edit.component.css']
})
export class SongEditComponent implements OnInit {
  songId: string;
  licenses$: License[];
  categories$: Observable<Category[]>
  nameMinLength = 2;
  nameMaxLength = 100;
  textMinLength = 10;
  textMaxLength = 1000;
  songForm: FormGroup;
  deleteForm: FormGroup;
  addedLicenses: Array<string> = [];
  musicLicenseTypesMapping = MusicLicenseTypeMapping;
  musicLicenseTypes = Object.keys(MusicLicenseType)
    .filter(k => !isNaN(Number(k)));

  visibleStatusMapping = VisibleStatusMapping;
  visibleStatus = Object.keys(VisibleStatus)
    .filter(k => !isNaN(Number(k)));;
  imageFileToUpload: File = null;
  audioFileToUpload: File = null;

  defaultImage: string = "songDefaultImage.jpg";
  imageUrl: string = "/assets/resources/song/images/" + this.defaultImage;
  mp3Url: string = "";

  isChangeImage: boolean = false;
  isChangeAudio: boolean = false;
  oldImage: string;
  oldAudio: string;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private categoryService: CategoryService,
    private licenseService: LicenseService,
    private musicLicenseService: MusicLicenseServiceService,
    private songService: SongService,
    private fileService: FileService,
    private authService: AuthService) {
    if (authService.isAuth == false) {
      this.router.navigate(['']);
    }

    this.categories$ = categoryService.all();
    licenseService.getOwnApproved()
      .subscribe(data => {
        this.licenses$ = data;
      });
  }

  ngOnInit() {
    let song: Song = this.route.snapshot.data.song;
    this.songId = song.id;
    this.addedLicenses = [];
    song.songLicenses.forEach(s => {
      this.addedLicenses.push(s.licenseId);
    });
    this.songForm = this.formBuilder.group({
      id: song.id,
      name: [
        song.name,
        [
          Validators.required,
          Validators.minLength(this.nameMinLength),
          Validators.maxLength(this.nameMaxLength)
        ]
      ],
      categoryId: [
        song.categoryId,
        [
          Validators.required,
          Validators.pattern('^((?!default).)*$'),
        ]
      ],
      text: [
        song.text,
        [
          Validators.required,
          Validators.minLength(this.textMinLength),
          Validators.maxLength(this.textMaxLength)
        ]
      ],
      imageFilePath: [
        song.imageFilePath,
        [
          //Validators.required,
        ]
      ],
      musicLicenseType: [
        song.musicLicenseType,
        Validators.required
      ],
      visibleStatus: [
        song.visibleStatus,
        Validators.required
      ],
      audioFilePath: [
        song.audioFilePath,
        [
          //Validators.required,
        ]
      ]
    });
    this.deleteForm = this.formBuilder.group({
      path: null,
      fileName: null,
    });
    this.imageUrl = song.imageFilePath;
    this.oldAudio = song.audioFilePath;
    this.oldImage = song.imageFilePath;
  }

  handleImageFileInput(file: FileList) {
    this.imageFileToUpload = file.item(0);
    this.isChangeImage = true;
    //Show image preview
    var reader = new FileReader();
    reader.onload = (event: any) => {
      this.imageUrl = event.target.result;
      this.songForm.get('imageFilePath').setValue(event.target.result);
    }
    reader.readAsDataURL(this.imageFileToUpload);
  }

  handleAudioFileInput(file: FileList) {
    this.audioFileToUpload = file.item(0);
    this.isChangeAudio = true;

    //Show image preview
    var reader = new FileReader();
    reader.onload = (event: any) => {
      this.mp3Url = event.target.result;
      this.songForm.get('audioFilePath').setValue(event.target.result);
    }
    reader.readAsDataURL(this.audioFileToUpload);
  }

  useDefaultPicture() {
    this.isChangeImage = true;
    this.imageUrl = "songDefaultImage.jpg";
    this.songForm.get('imageFilePath').setValue(this.imageUrl);
  }

  submit() {
    this.uploadChanges();
  }

  get name(): AbstractControl {
    return this.songForm.get('name');
  }

  get text(): AbstractControl {
    return this.songForm.get('text');
  }

  get categoryId(): AbstractControl {
    return this.songForm.get('categoryId');
  }

  get imageFilePath(): AbstractControl {
    return this.songForm.get('imageFilePath');
  }

  get audioFilePath(): AbstractControl {
    return this.songForm.get('audioFilePath');
  }

  private uploadChanges() {
    this.UploadAudio();
  }

  addLicense(licenseId: string) {
    var index = this.addedLicenses.indexOf(licenseId);
    if (index > -1) {
      this.addedLicenses.splice(index, 1);
    }
    else {
      this.addedLicenses.push(licenseId);
    }
  }

  get musicLicenseType(): AbstractControl {
    return this.songForm.get('musicLicenseType');
  }

  get visibleStatusForm(): AbstractControl {
    return this.songForm.get('visibleStatus');
  }

  private UploadImage() {
    if (this.isChangeImage) {
      if (this.oldImage != this.defaultImage) {
        this.songService.getById(this.songId).subscribe(songData => {
          this.deleteForm.get('fileName').setValue(songData.audioFilePath);
          this.deleteForm.get('path').setValue(globalConstants.songAudioPath);
          let audioFile: FileInfo = this.deleteForm.value;

          this.fileService.deleteFile(audioFile).subscribe(_ => { });
        });
      }
      this.imageFilePath.setValue(this.defaultImage);
      if (this.imageFileToUpload != null) {
        this.fileService.create(this.audioFileToUpload, globalConstants.songImagePath)
          .subscribe(data => {
            this.imageFilePath.setValue(data["fileName"])
            this.editSong();
          });
      }
      else {
        this.editSong();
      }
    }
    else {
      this.editSong();
    }
  }

  private UploadAudio() {
    if (this.isChangeAudio) {
      this.audioFilePath.setValue(this.defaultImage);
      if (this.imageFileToUpload != null) {
        this.fileService.create(this.audioFileToUpload, globalConstants.songAudioPath)
          .subscribe(data => {
            this.audioFilePath.setValue(data["fileName"])
          });
      }
    }
    else {
      this.UploadImage();
    }
  }

  private editSong() {
    this.musicLicenseService.deleteAllMusicLicenses(this.songId).subscribe(_ => {
      let song: Song = this.songForm.value;
      if (song.visibleStatus == 0) {
        song.visibleStatus = VisibleStatus.Public;
      }
      else if (song.visibleStatus == 1) {
        song.visibleStatus = VisibleStatus.Hidden;
      }
      else {
        song.visibleStatus = VisibleStatus.OnlyMe;
      }

      if (song.musicLicenseType == 0) {
        song.musicLicenseType = MusicLicenseType.CC;
      }
      else if (song.musicLicenseType == 1) {
        song.musicLicenseType = MusicLicenseType.CCBY;
      }
      else if (song.musicLicenseType == 2) {
        song.musicLicenseType = MusicLicenseType.CCBYSA;
      }
      else {
        song.musicLicenseType = MusicLicenseType.CCBYNDSA;
      }

      var licenseContorls = this.addedLicenses;
      this.songService.edit(song, this.songId)
        .subscribe(_ => {
          licenseContorls.forEach(licenseId => {
            let musicLicense = {
              licenseId: licenseId,
              songId: this.songId,
            } as MusicLicense;
            this.musicLicenseService.create(musicLicense).subscribe(_ => _);
          });
          this.router.navigate(['song/own']);
          this.songForm.reset();
        });
    }); 
  }

  isLicenseChecked(license: License): boolean{
    return this.addedLicenses.some(x => x == license.id);
  }
}
