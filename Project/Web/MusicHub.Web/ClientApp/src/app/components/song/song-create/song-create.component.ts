import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { FormGroup, FormBuilder, Validators, AbstractControl, FormArray, FormControl } from '@angular/forms';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { HttpEventType, HttpRequest, HttpClient } from '@angular/common/http';
import { readFileSync, appendFile, copyFileSync } from 'fs';
import { FileDetector } from 'protractor';
import { ParseSourceFile } from '@angular/compiler';
import Category from '../../shared/models/category';
import { CategoryService } from '../../../core/services/category.service';
import { SongService } from '../../../core/services/song.service';
import { AuthService } from '../../../core/services/auth.service';
import { Song } from '../../shared/models/song';
import { FileService } from '../../../core/services/file.service';
import { globalConstants } from '../../../common/global-constants';
import { LicenseService } from '../../../core/services/license.service';
import License from '../../shared/models/license';
import { MusicLicenseType, MusicLicenseTypeMapping } from '../../shared/models/musicLicenseType';
import { VisibleStatus, VisibleStatusMapping } from '../../shared/models/visibleStatus';
import { MusicLicenseServiceService } from '../../../core/services/music-license-service.service';
import MusicLicense from '../../shared/models/musicLicense';
import { strictEqual } from 'assert';

@Component({
  selector: 'app-song-create',
  templateUrl: './song-create.component.html',
  styleUrls: ['./song-create.component.css']
})
export class SongCreateComponent implements OnInit {
  categories$: Observable<Category[]>
  licenses$: License[];
  nameMinLength = 2;
  nameMaxLength = 100;
  textMinLength = 10;
  textMaxLength = 1000;
  songForm: FormGroup;
  addedLicenses: Array<string> = [];
  musicLicenseTypesMapping = MusicLicenseTypeMapping;
  musicLicenseTypes = Object.keys(MusicLicenseType)
    .filter(k => !isNaN(Number(k)));

  visibleStatusMapping = VisibleStatusMapping;
  visibleStatus = Object.keys(VisibleStatus)
    .filter(k => !isNaN(Number(k)));;
  defaultImage: string = "songDefaultImage.jpg";
  imageUrl: string = "/assets/resources/song/images/" + this.defaultImage;
  mp3Url: string = "";

  imageFileToUpload: File = null;
  audioFileToUpload: File = null;
  
  error: string;
  userId: number = 1;
  uploadResponse = { status: '', message: '', filePath: '' };
  @Output() public onUploadFinished = new EventEmitter();

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
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
    this.songForm = this.formBuilder.group({
      name: [
        null,
        [
          Validators.required,
          Validators.minLength(this.nameMinLength),
          Validators.maxLength(this.nameMaxLength)
        ]
      ],
      categoryId: [
        'default',
        [
          Validators.required,
          Validators.pattern('^((?!default).)*$'),
        ]
      ],
      text: [
        null,
        [
          Validators.required,
          Validators.minLength(this.textMinLength),
          Validators.maxLength(this.textMaxLength)
        ]
      ],
      imageFilePath: [
        this.defaultImage,
      ],
      musicLicenseType: [
        MusicLicenseType.CC,
        Validators.required
      ],
      visibleStatus: [
        VisibleStatus.Public,
        Validators.required
      ],
      audioFilePath: [
        null,
        [
          Validators.required,
        ]
      ]
    })
  }   

  handleImageFileInput(file: FileList) {
    this.imageFileToUpload = file.item(0);

    //Show image preview
    var reader = new FileReader();
    reader.onload = (event: any) => {
      this.imageUrl = event.target.result;
      this.imageFilePath.setValue(event.target.result);
    }
    reader.readAsDataURL(this.imageFileToUpload);
  }

  handleAudioFileInput(file: FileList) {
    this.audioFileToUpload = file.item(0);

    //Show image preview
    var reader = new FileReader();
    reader.onload = (event: any) => {
      this.mp3Url = event.target.result;
      this.audioFilePath.setValue(event.target.result);
    }
    reader.readAsDataURL(this.audioFileToUpload);
  }

  useDefaultPicture() {
    this.imageUrl = "../../../../../assets/resources/song/images/" + this.defaultImage;
    this.imageFilePath.setValue(this.defaultImage);
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

  submit() {
    this.fileService.create(this.audioFileToUpload, globalConstants.songAudioPath)
      .subscribe(data => {
        this.audioFilePath.setValue(data["fileName"]);
        this.imageFilePath.setValue(this.defaultImage);
        if (this.imageFileToUpload != null) {
          this.fileService.create(this.imageFileToUpload, globalConstants.songImagePath)
            .subscribe(data => {
              this.imageFilePath.setValue(data["fileName"]);
              this.createSong();
            });
        }
        else {
          this.createSong();
        }
      });
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

  get musicLicenseType(): AbstractControl {
    return this.songForm.get('musicLicenseType');
  }

  get visibleStatusForm(): AbstractControl {
    return this.songForm.get('visibleStatus');
  }

  private createSong() {
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
    this.songService.create(song)
      .subscribe(data => {
        licenseContorls.forEach(licenseId => {
          let musicLicense =  {
            licenseId : licenseId,
            songId: data["songId"],
          } as MusicLicense ;
          this.musicLicenseService.create(musicLicense).subscribe(_ => _);
        });
        this.router.navigate(['song/own']);
        this.songForm.reset();
      });
  }
}
