import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import Category from '../../../categories/models/category';
import { CategoryService } from '../../../categories/services/category.service';
import { globalConstants } from '../../../common/global-constants';
import { AuthService } from '../../../core/services/auth.service';
import { MegaStorageFileService } from '../../../core/services/mega-storage-file.service';
import { CreateMusicLicenseType, MusicLicenseType, MusicLicenseTypeMapping } from '../../../shared/models/musicLicenseType';
import { CreateVisibleStatus, VisibleStatus, VisibleStatusMapping } from '../../../shared/models/visibleStatus';
import { Music } from '../../models/music';
import { MusicService } from '../../services/music.service';

@Component({
  selector: 'app-music-create',
  templateUrl: './music-create.component.html',
  styleUrls: ['./music-create.component.css']
})
export class MusicCreateComponent implements OnInit {
  categories$: Observable<Category[]>
 // licenses$: License[];
  nameMinLength = 2;
  nameMaxLength = 100;
  descriptionMinLength = 10;
  descriptionMaxLength = 1000;
  musicForm: FormGroup;
  addedLicenses: Array<string> = [];
  musicLicenseTypesMapping = MusicLicenseTypeMapping;
  musicLicenseTypes = Object.keys(MusicLicenseType)
    .filter(k => !isNaN(Number(k)));

  visibleStatusMapping = VisibleStatusMapping;
  visibleStatus = Object.keys(VisibleStatus)
    .filter(k => !isNaN(Number(k)));;
  musicFolderPath: string = globalConstants.music.imageFolderPath;
  defauleMusicImagePath: string = this.musicFolderPath + globalConstants.music.defauldImage;
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
    // private licenseService: LicenseService,
    //private musicLicenseService: MusicLicenseServiceService,
    private musicService: MusicService,
    private fileService: MegaStorageFileService,
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
    console.log(this.defauleMusicImagePath);
    this.musicForm = this.formBuilder.group({
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
      description: [
        null,
        [
          Validators.required,
          Validators.minLength(this.descriptionMinLength),
          Validators.maxLength(this.descriptionMaxLength)
        ]
      ],
      imageUrl: [
        this.defauleMusicImagePath,
      ],
      audioUrl: [
        null,
       // Validators.required
      ],
      musicLicenseType:[
        MusicLicenseType.CC,
        Validators.required
      ],
      visibleStatus: [
        VisibleStatus.Public,
        Validators.required
      ],
      audioFilePath: [
        null,
        /*[
          Validators.required,
        ]*/
      ]
    })
  }   

  handleImageFileInput(file: FileList) {
    this.imageFileToUpload = file.item(0);

    //Show image preview
    var reader = new FileReader();
    reader.onload = (event: any) => {
      this.defauleMusicImagePath = event.target.result;
      this.imageUrl.setValue(event.target.result);
    }
    reader.readAsDataURL(this.imageFileToUpload);
  }

  handleAudioFileInput(file: FileList) {
    this.audioFileToUpload = file.item(0);

    //Show image preview
    var reader = new FileReader();
    reader.onload = (event: any) => {
      this.mp3Url = event.target.result;
      this.audioUrl.setValue(event.target.result);
    }
    reader.readAsDataURL(this.audioFileToUpload);
  }

  useDefaultPicture() {
    this.defauleMusicImagePath = this.musicFolderPath + globalConstants.music.defauldImage;
    this.imageUrl.setValue(this.defauleMusicImagePath);
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
    this.fileService.create(this.audioFileToUpload, globalConstants.music.audioFolderPath)
      .subscribe(data => {
        this.audioUrl.setValue(data["fileName"]);
        this.imageUrl.setValue(this.defauleMusicImagePath);
        if (this.imageFileToUpload != null) {
          this.fileService.create(this.imageFileToUpload, globalConstants.music.imageFolderPathToUpload)
            .subscribe(data => {
              this.imageUrl.setValue(data["fileName"]);
              this.createMusic();
                });
            }
        else {
          this.createMusic();
        }
   });
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

  private createMusic() {
    let music: Music = this.musicForm.value;
    music.visibleStatus = CreateVisibleStatus(music.visibleStatus);
    music.musicLicenseType = CreateMusicLicenseType(music.musicLicenseType);

    var licenseContorls = this.addedLicenses;
    this.musicService.create(music)
      .subscribe(data => {
       /* licenseContorls.forEach(licenseId => {
          let musicLicense = {
            licenseId: licenseId,
            songId: data["songId"],
          } as MusicLicense;
          this.musicLicenseService.create(musicLicense).subscribe(_ => _);
        });*/
        this.router.navigate(['musics/own']);
        this.musicForm.reset();
      });
  }
}
