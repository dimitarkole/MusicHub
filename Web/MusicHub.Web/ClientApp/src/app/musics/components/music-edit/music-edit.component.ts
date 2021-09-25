import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import Category from '../../../categories/models/category';
import { CategoryService } from '../../../categories/services/category.service';
import { AuthService } from '../../../core/services/auth.service';
import FileInfo from '../../../shared/models/fileInfo';
//import { CreateMusicLicenseType, MusicLicenseType, MusicLicenseTypeMapping } from '../../../shared/models/musicLicenseType';
import { Music } from '../../models/music';
import { MusicService } from '../../services/music.service';

@Component({
  selector: 'app-music-edit',
  templateUrl: './music-edit.component.html',
  styleUrls: ['./music-edit.component.css']
})
export class MusicEditComponent implements OnInit {
  categories$: Observable<Category[]>
  musicId: string;
  // licenses$: License[];
  nameMinLength = 2;
  nameMaxLength = 100;
  descriptionMinLength = 10;
  descriptionMaxLength = 1000;
  musicForm: FormGroup;
  deleteFileForm: FormGroup;
  addedLicenses: Array<string> = [];
/*  musicLicenseTypesMapping = MusicLicenseTypeMapping;
  musicLicenseTypes = Object.keys(MusicLicenseType)
    .filter(k => !isNaN(Number(k)));*/

  /*visibleStatusMapping = VisibleStatusMapping;
  visibleStatus = Object.keys(VisibleStatus)
    .filter(k => !isNaN(Number(k)));;*/
  defaultImage: string = "songDefaultImage.jpg";
  imageUrlDefault: string = "/assets/resources/song/images/" + this.defaultImage;
  mp3Url: string = "";

  imageFileToUpload: File = null;
  audioFileToUpload: File = null;

  error: string;
  userId: number = 1;
  uploadResponse = { status: '', message: '', filePath: '' };
  @Output() public onUploadFinished = new EventEmitter();
  oldImage: string;
  oldAudio: string;
  isChangeImage: boolean = false;
  isChangeAudio: boolean = false;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private categoryService: CategoryService,
    // private licenseService: LicenseService,
    // private musicLicenseService: MusicLicenseServiceService,
    private musicService: MusicService,
    // private fileService: FileService,
    private authService: AuthService) {
    if (authService.isAuth == false) {
      this.router.navigate(['']);
    }

    this.categories$ = categoryService.all();
    /*licenseService.getOwnApproved()
      .subscribe(data => {
        this.licenses$ = data;
      });*/
  }

  ngOnInit() {
    let music: Music = this.route.snapshot.data.music;
    this.musicId = music.id;
    this.addedLicenses = [];
    /* music.songLicenses.forEach(s => {
       this.addedLicenses.push(s.licenseId);
     });*/
    this.musicForm = this.formBuilder.group({
      id: music.id,
      name: [
        music.name,
        [
          Validators.required,
          Validators.minLength(this.nameMinLength),
          Validators.maxLength(this.nameMaxLength)
        ]
      ],
      categoryId: [
        music.categoryId,
        [
          Validators.required,
          Validators.pattern('^((?!default).)*$'),
        ]
      ],
      description: [
        music.description,
        [
          Validators.required,
          Validators.minLength(this.descriptionMinLength),
          Validators.maxLength(this.descriptionMaxLength)
        ]
      ],
      imageUrl: [
        music.imageUrl,
        [
          //Validators.required,
        ]
      ],
      musicLicenseType: [
        Validators.required
      ],
      visibleStatus: [
        Validators.required
      ],
      audioUrl: [
        music.audioUrl,
        [
          //Validators.required,
        ]
      ]
    });
    this.deleteFileForm = this.formBuilder.group({
      fileUrl: null,
    });
    this.imageUrlDefault = music.imageUrl;
    this.oldAudio = music.audioUrl;
    this.oldImage = music.imageUrl;
  }

  handleImageFileInput(file: FileList) {
    this.imageFileToUpload = file.item(0);
    this.isChangeImage = true;
    //Show image preview
    var reader = new FileReader();
    reader.onload = (event: any) => {
      this.imageUrlDefault = event.target.result;
      this.musicForm.get('imageUrl').setValue(event.target.result);
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
      this.musicForm.get('audioUrl').setValue(event.target.result);
    }
    reader.readAsDataURL(this.audioFileToUpload);
  }

  get name(): AbstractControl {
    return this.musicForm.get('name');
  }

  get description(): AbstractControl {
    return this.musicForm.get('description');
  }

  get categoryId(): AbstractControl {
    return this.musicForm.get('categoryId');
  }

  get imageUrl(): AbstractControl {
    return this.musicForm.get('imageUrl');
  }

  get audioUrl(): AbstractControl {
    return this.musicForm.get('audioUrl');
  }

  get musicLicenseType(): AbstractControl {
    return this.musicForm.get('musicLicenseType');
  }

  get visibleStatusForm(): AbstractControl {
    return this.musicForm.get('visibleStatus');
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

  useDefaultPicture() {
    this.isChangeImage = true;
    this.imageUrlDefault = "songDefaultImage.jpg";
    this.musicForm.get('imageUrl').setValue(this.imageUrlDefault);
  }


  submit() {
    this.uploadChanges();
  }

  private uploadChanges() {
    this.UploadAudio();
  }

  private UploadAudio() {
    if (this.isChangeAudio) {
      this.audioUrl.setValue(this.defaultImage);
      if (this.imageFileToUpload != null) {
        /*this.fileService.create(this.audioFileToUpload, globalConstants.songAudioPath)
          .subscribe(data => {
            this.audioFilePath.setValue(data["fileName"])
          });*/
      }
  }

    this.UploadImage();
  }

  private UploadImage() {
    if (this.isChangeImage) {
      if (this.oldImage != this.defaultImage) {
        this.musicService.getById(this.musicId).subscribe(musicData => {
          this.deleteFileForm.get('fileUrl').setValue(musicData.imageUrl);
          let imageFile: FileInfo = this.deleteFileForm.value;

          // this.fileService.deleteFile(audioFile).subscribe(_ => { });
        });
      }
      this.imageUrl.setValue(this.defaultImage);
      if (this.imageFileToUpload != null) {
        /* this.fileService.create(this.audioFileToUpload, globalConstants.songImagePath)
           .subscribe(data => {
             this.imageFilePath.setValue(data["fileName"])
             this.editSong();
           });*/
      }
      else {
        this.editSong();
      }
    }
    else {
      this.editSong();
    }
  }

  private editSong() {
    let music: Music = this.musicForm.value;
    // music.musicLicenseType = CreateMusicLicenseType(music.musicLicenseType);

    var licenseContorls = this.addedLicenses;
    this.musicService.edit(music, this.musicId)
      .subscribe(_ => {
        /*licenseContorls.forEach(licenseId => {
          let musicLicense = {
            licenseId: licenseId,
            songId: this.songId,
          } as MusicLicense;
          this.musicLicenseService.create(musicLicense).subscribe(_ => _);
        });*/
        this.router.navigate(['musics/own']);
        this.musicForm.reset();
      });
  }

 /* isLicenseChecked(license: License): boolean {
    return this.addedLicenses.some(x => x == license.id);
  }*/
}
