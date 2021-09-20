import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import Category from '../../../categories/models/category';
import { CategoryService } from '../../../categories/services/category.service';
import { globalConstants } from '../../../common/global-constants';
import { requiredFileType } from '../../../common/requiredFileType.validator';
import { AuthService } from '../../../core/services/auth.service';
import { MegaStorageFileService } from '../../../core/services/mega-storage-file.service';
import { CreateVisibleStatus, VisibleStatus, VisibleStatusMapping } from '../../../shared/models/visibleStatus';
import { Music } from '../../models/music';
import { MusicService } from '../../services/music.service';

@Component({
  selector: 'app-music-create',
  templateUrl: './music-create.component.html',
  styleUrls: ['./music-create.component.css']
})
export class MusicCreateComponent implements OnInit {
  activeStep: number = 1;
  operation: string = "Create";
  imageFileToUpload: File = null;
  audioFileToUpload: File = null;
  musicFolderPath: string = globalConstants.music.imageFolderPathToUpload;
  defauleMusicImagePath: string = this.musicFolderPath + globalConstants.music.defauldImage;
  audioPath: string = "";
  musicForm: FormGroup;
  categories$: Observable<Category[]>
  nameMinLength = globalConstants.music.nameMinLength;
  nameMaxLength = globalConstants.music.nameMaxLength;
  descriptionMinLength = globalConstants.music.nameMinLength;
  descriptionMaxLength = globalConstants.music.nameMaxLength;
  musicId: string = "";
  constructor(
    private formBuilder: FormBuilder,
    private categoryService: CategoryService,
    private musicService: MusicService,
    private route: ActivatedRoute,
    private router: Router,
    private fileService: MegaStorageFileService,
  ) {
    this.categories$ = categoryService.all();
  }

  ngOnInit() {
    var music: Music = {
      categoryId : 'default',
      imageUrl : this.defauleMusicImagePath,
      privacy : "Public",
      containsExplicitContent : "yes",
      displayEmbedCode : true,
      offlineListening : true,
      appPlayback : true,
      rSSfeed : false,
      containsMusic : "yes",
      license : "AllRightsReserved",
      attribution : true,
      noncommercial : true,
      noDerivativeWorks : false,
      shareALike : true
    }

    if (this.router.url.includes("edit")) {
      music = this.route.snapshot.data.music;
      this.operation = "edit";
      this.activeStep = 2;
      this.musicId = music.id;
    }

    this.musicForm = this.formBuilder.group({
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
        music.description ,
        [
          Validators.required,
          Validators.minLength(this.descriptionMinLength),
          Validators.maxLength(this.descriptionMaxLength)
        ]
      ],
      imageUrl: [
        music.imageUrl,
        [
          Validators.required,
          requiredFileType("image"),
        ]
      ],
      audioUrl: [
        music.audioUrl,
        [
          Validators.required,
          requiredFileType("audio"),
        ]
      ],
      privacy: [
        music.privacy,
        Validators.required
      ],
      buyLink: [
        music.buyLink
      ],
      albumTitle: [
        music.albumTitle
      ],
      recordLabel: [
        music.recordLabel
      ],
      releaseDate: [
        music.releaseDate
      ],
      barcode: [
        music.barcode
      ],
      iSWC: [
        music.iSWC
      ],
      pLine: [
        music.pLine
      ],
      containsExplicitContent: [
        music.containsExplicitContent
      ],
      enableDirectDownloads: [
        music.enableDirectDownloads,
      ],
      displayEmbedCode: [
        music.displayEmbedCode,
      ],
      offlineListening: [
        music.offlineListening,
      ],
      appPlayback: [
        music.appPlayback,
      ],
      rSSfeed: [
        music.rSSfeed,
      ],
      containsMusic: [
        music.containsMusic,
      ],
      artist: [
        music.artist,
      ],
      publisher: [
        music.publisher,
      ],
      iSRC: [
        music.iSRC,
      ],
      composer: [
        music.composer,
      ],
      releaseTitle: [
        music.releaseTitle,
      ],
      license: [
        music.license,
      ],
      attribution: [
        music.attribution,
      ],
      noncommercial: [
        music.noncommercial,
      ],
      noDerivativeWorks: [
        music.noDerivativeWorks,
      ],
      shareALike: [
        music.shareALike,
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
      var audioName = this.audioFileToUpload.name;
      this.name.setValue(audioName.substring(0, audioName.lastIndexOf('.')));
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
    this.imageUrl.setValue(this.defauleMusicImagePath);
  }

  submit() {
    alert("here")

    if (this.router.url.includes("edit")) {
      this.editMusic();
    }
    else {
      this.fileService.create(this.audioFileToUpload, globalConstants.music.audioFolderPath)
        .subscribe(data => {
          this.audioUrl.setValue(data["fileName"]);
          this.imageUrl.setValue(this.defauleMusicImagePath);
          if (this.imageFileToUpload != null) {
            this.fileService.create(this.imageFileToUpload, globalConstants.music.imageFolderPath)
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

  get shareALike(): AbstractControl {
    return this.musicForm.get('shareALike');
  }

  // is audio uploaded
  isFirstStepFilled() {
    var result = true;
    if (this.audioUrl.value == null) {
      this.audioUrl.markAsTouched();
      this.audioUrl.setErrors({ 'required': true });
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
    var result = true;
    if (this.name.valid == false) {
      this.name.markAsTouched();
      result = false;
    }

    if (this.categoryId.valid == false) {
      this.categoryId.markAsTouched();
      result = false;
    }

    if (this.description.valid == false) {
      this.description.markAsTouched();
      result = false;
    }

    if (this.privacy.valid == false) {
      this.privacy.markAsTouched();
      result = false;
    }

    return result;
  }

  // is metadata Filled
  isFourthStepFilled() {
    return true;
  }

  // is permissions Filled
  isFifthStepFilled() {
    return true;
  }

  private createMusic() {
    let music: Music = this.musicForm.value;
    //music.visibleStatus = CreateVisibleStatus(music.visibleStatus);
    //music.musicLicenseType = CreateMusicLicenseType(music.musicLicenseType);

    this.musicService.create(music)
      .subscribe(data => {
        this.router.navigate(['musics/own']);
        this.musicForm.reset();
      });
  }

  private editMusic() {
    let music: Music = this.musicForm.value;
    //music.visibleStatus = CreateVisibleStatus(music.visibleStatus);
    //music.musicLicenseType = CreateMusicLicenseType(music.musicLicenseType);

    this.musicService.edit(music, this.musicId)
      .subscribe(data => {
        this.router.navigate(['musics/own']);
        this.musicForm.reset();
      });
  }

  shareALikeIsChange() {
    if (this.shareALike.value == true && this.noDerivativeWorks.value == true) this.noDerivativeWorks.setValue(false);
  }

  noDerivativeWorksIsChange() {
    if (this.noDerivativeWorks.value == true && this.shareALike.value == true) this.shareALike.setValue(false);
  }
}
