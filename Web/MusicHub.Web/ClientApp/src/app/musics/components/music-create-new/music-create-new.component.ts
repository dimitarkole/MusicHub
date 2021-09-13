import { validateVerticalPosition } from '@angular/cdk/overlay';
import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import Category from '../../../categories/models/category';
import { CategoryService } from '../../../categories/services/category.service';
import { globalConstants } from '../../../common/global-constants';
import { requiredFileType } from '../../../common/requiredFileType.validator';
import { MegaStorageFileService } from '../../../core/services/mega-storage-file.service';
import { LicenseStatus } from '../../../licenses/models/LicenseStatus';
import { MusicLicenseType } from '../../../shared/models/musicLicenseType';
import { VisibleStatus, VisibleStatusMapping } from '../../../shared/models/visibleStatus';

@Component({
  selector: 'app-music-create-new',
  templateUrl: './music-create-new.component.html',
  styleUrls: ['./music-create-new.component.css']
})
export class MusicCreateNewComponent implements OnInit {
  activeStep: number = 2;
  imageFileToUpload: File = null;
  audioFileToUpload: File = null;
  musicFolderPath: string = globalConstants.music.imageFolderPath;
  defauleMusicImagePath: string = this.musicFolderPath + globalConstants.music.defauldImage;
  audioPath: string = "";
  musicForm: FormGroup;
  categories$: Observable<Category[]>

  constructor(
    private formBuilder: FormBuilder,
    private categoryService: CategoryService,
    private fileService: MegaStorageFileService,
  ) {
    this.categories$ = categoryService.all();
  }

  ngOnInit() {
    this.musicForm = this.formBuilder.group({
      name: [
        null,
        [
          Validators.required,
          Validators.minLength(globalConstants.music.nameMinLength),
          Validators.maxLength(globalConstants.music.nameMaxLength)
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
          Validators.minLength(globalConstants.music.descriptionMinLength),
          Validators.maxLength(globalConstants.music.descriptionMaxLength)
        ]
      ],
      imageUrl: [
        this.defauleMusicImagePath,
        [
          Validators.required,
          requiredFileType("image"),
        ]
      ],
      audioUrl: [
        null,
        [
          Validators.required,
          requiredFileType("audio"),
        ]
      ],
      privacy: [
        "Public",
        Validators.required
      ],
      buyLink: [
        null,
      ],
      albumTitle: [
        null,
      ],
      recordLabel: [
        null,
      ],
      releaseDate: [
        null,
      ],
      barcode: [
        null,
      ],
      iSWC: [
        null,
      ],
      pLine: [
        null,
      ],
      containsExplicitContent: [
        "yes", 
      ],
      enableDirectDownloads:[
        false,
        Validators.required
      ],
      displayEmbedCode: [
        true,
        Validators.required
      ],
      offlineListening: [
        true,
        Validators.required
      ],
      appPlayback: [
        true,
        Validators.required
      ], 
      rSSfeed: [
        false,
        Validators.required
      ],
      containsMusic: [
        "yes",
        Validators.required
      ],
      artist: [
        null,
      ],
      publisher: [
        null,
      ],
      iSRC: [
        null,
      ],
      composer: [
        null,
      ],
      releaseTitle: [
        null,
      ],
      license: [
        "AllRightsReserved"
      ],
      attribution: [
        true
      ],
      noncommercial: [
        true
      ],
      noDerivativeWorks: [
        false
      ],
      shareAlike: [
        true
      ],
    })
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

  handleAudioFileInput(file: FileList) {
    this.audioFileToUpload = file.item(0);
    //Show image preview
    var reader = new FileReader();
    reader.onload = (event: any) => {
      this.audioPath = event.target.result;
      this.audioUrl.setValue(event.target.result);
    }
    reader.readAsDataURL(this.audioFileToUpload);
  }

  uploadAudio() {
    this.fileService.create(this.audioFileToUpload, globalConstants.music.audioFolderPath)
      .subscribe(data => {
        this.audioUrl.setValue(data["fileName"]);
       // this.isAudioFileUpload = true;
      });
  }

  uploadImage() {
    this.imageUrl.setValue(this.defauleMusicImagePath);
    if (this.imageFileToUpload != null) {
      this.fileService.create(this.imageFileToUpload, globalConstants.music.imageFolderPathToUpload)
        .subscribe(data => {
          this.imageUrl.setValue(data["fileName"]);
        });
    }
  }

  useDefaultPicture() {
    alert("here");
    this.imageUrl.setValue(this.defauleMusicImagePath);
    alert(this.imageUrl.value);

  }

  submit() {

  }

  nextStep() {
    var isActiveStepFilled: boolean = false;
    switch (this.activeStep) {
      case 1: isActiveStepFilled = this.isFirstStepFilled(); break;
      case 2: isActiveStepFilled = this.isSecondStepFilled(); break;
      case 3: isActiveStepFilled = this.isThirdStepFilled(); break;
      case 4: isActiveStepFilled = this.isFourthStepFilled(); break;
      case 5: isActiveStepFilled = this.isFifthStepFilled(); break;
      default:
    }

    if (isActiveStepFilled == true) this.activeStep++;
  }

  previousStep() {
    this.activeStep--;
  }

  get name(): AbstractControl {
    return this.musicForm.get('name');
  }

  get appPlayback(): AbstractControl {
    return this.musicForm.get('appPlayback');
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

  get privacy(): AbstractControl {
    return this.musicForm.get('privacy');
  }

  get enableDirectDownloads(): AbstractControl {
    return this.musicForm.get('enableDirectDownloads');
  }

  get offlineListening(): AbstractControl {
    return this.musicForm.get('offlineListening');
  }

  get rSSfeed(): AbstractControl {
    return this.musicForm.get('rSSfeed');
  }
  
  get containsMusic(): AbstractControl {
    return this.musicForm.get('containsMusic');
  }

  get artist(): AbstractControl {
    return this.musicForm.get('artist');
  }

 get publisher(): AbstractControl {
    return this.musicForm.get('publisher');
  }

 get iSRC(): AbstractControl {
    return this.musicForm.get('iSRC');
  }

 get composer(): AbstractControl {
    return this.musicForm.get('composer');
  }

 get releaseTitle(): AbstractControl {
    return this.musicForm.get('releaseTitle');
  }

  get displayEmbedCode(): AbstractControl {
    return this.musicForm.get('displayEmbedCode');
  }

  get buyLink(): AbstractControl {
    return this.musicForm.get('buyLink');
  }

  get albumTitle(): AbstractControl {
    return this.musicForm.get('albumTitle');
  }

  get recordLabel(): AbstractControl {
    return this.musicForm.get('recordLabel');
  }

  get releaseDate(): AbstractControl {
    return this.musicForm.get('releaseDate');
  }

  get barcode(): AbstractControl {
    return this.musicForm.get('barcode');
  }

  get iSWC(): AbstractControl {
    return this.musicForm.get('iSWC');
  }

  get pLine(): AbstractControl {
    return this.musicForm.get('pLine');
  }

  get containsExplicitContent(): AbstractControl {
    return this.musicForm.get('containsExplicitContent');
  }

  get license(): AbstractControl {
    return this.musicForm.get('license');
  }

  get attribution(): AbstractControl {
    return this.musicForm.get('attribution');
  }

  get noncommercial(): AbstractControl {
    return this.musicForm.get('noncommercial');
  }

  get noDerivativeWorks(): AbstractControl {
    return this.musicForm.get('noDerivativeWorks');
  }

  get shareAlike(): AbstractControl {
    return this.musicForm.get('shareAlike');
  }

  // is audio uploaded
  isFirstStepFilled() {
    var result = true;
    if (this.audioUrl.value == null) {
      this.audioUrl.markAsTouched();
      this.audioUrl.setErrors({'required': true});
      result = false;
    }

    if (this.audioUrl.hasError('requiredFileType')) {
      this.audioUrl.markAsTouched();
      result = false;
    }

    return result;
  }

  // is image uploaded
  isSecondStepFilled() {
    var result = true;
    if (this.imageUrl.value == null) {
      this.imageUrl.markAsTouched();
      this.imageUrl.setErrors({ 'required': true });
      result = false;
    }

    if (this.imageUrl.hasError('requiredFileType')) {
      this.imageUrl.markAsTouched();
      result = false;
    }

    return result;
  }

  // is basic info Filled
  isThirdStepFilled() {
    return true;
  }

  // is metadata Filled
  isFourthStepFilled() {
    return true;
  }

  // is permissions Filled
  isFifthStepFilled() {
    return true;
  }
}
