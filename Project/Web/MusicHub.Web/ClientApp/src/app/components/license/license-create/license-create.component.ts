import { ArrayType } from '@angular/compiler';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { globalConstants } from '../../../common/global-constants';
import { modelConstants } from '../../../common/model-constants';
import { AuthService } from '../../../core/services/auth.service';
import { CategoryService } from '../../../core/services/category.service';
import { FileService } from '../../../core/services/file.service';
import { LicenseService } from '../../../core/services/license.service';
import { SongService } from '../../../core/services/song.service';
import Category from '../../shared/models/category';
import License from '../../shared/models/license';
import LicenseFile from '../../shared/models/licenseFile';
import { Song } from '../../shared/models/song';

@Component({
  selector: 'app-license-create',
  templateUrl: './license-create.component.html',
  styleUrls: ['./license-create.component.css']
})
export class LicenseCreateComponent implements OnInit {
  nameMinLength = modelConstants.licese.nameMinLength;
  nameMaxLength = modelConstants.licese.nameMaxLength;
  licenseForm: FormGroup;
  imageFiles: Array<File> = null;
  imageUrls: string[];
  imageFileMaxCount: number = modelConstants.licese.imageFileMaxCount;
  error: string;
  userId: number = 1;
  uploadResponse = { status: '', message: '', filePath: '' };
  @Output() public onUploadFinished = new EventEmitter();

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private fileService: FileService,
    private licenseService: LicenseService,
    private authService: AuthService) {
    this.imageUrls = [];
    if (authService.isAuth == false) {
      this.router.navigate(['']);
    }
  }

  ngOnInit() {
    this.licenseForm = this.formBuilder.group({
      name: [
        null,
        [
          Validators.required,
          Validators.minLength(this.nameMinLength),
          Validators.maxLength(this.nameMaxLength)
        ]
      ],
      uploadfiles: [
        this.formBuilder.array(Array<File>()),
      ],
    })
  }

  handleFileInput(file: FileList) {
    var newFile = file.item(0);
    //Show image preview
    this.licenseForm.get('uploadfiles').value.push(this.formBuilder.group({
      newFile
    }));

    //Show image preview
    var reader = new FileReader();
    reader.onload = (event: any) => {
      this.imageUrls.push(event.target.result);
    }
    reader.readAsDataURL(newFile);
  }

  submit() {
    var contorls = (this.licenseForm.get('uploadfiles').value as FormArray).value;
    var fileNames: string[] = []; 
    let license = { name: this.name.value };

    this.licenseService.create(license as License)
      .subscribe(licenseId => {
        contorls.forEach(file => {
          console.log(file);
          this.fileService.create(file['newFile'] as File, globalConstants.licensePath)
            .subscribe(name => {
              let licenseFile = { path: name, licenseId: licenseId };
              this.licenseService.createFile(licenseFile as LicenseFile).subscribe(_ => _);
            });
        });
      });

   
    console.log(fileNames);

    this.createLicense(fileNames);
  }

  removeFile(index: number) {
    this.licenseForm.get('files').value.removeAt(index);
    this.imageUrls.splice(index, 1);
  }

  get name(): AbstractControl {
    return this.licenseForm.get('name');
  }

  get files() {
    return this.licenseForm.get('uploadfiles') as FormArray;
  }

  private createLicense(filesPath: string[]) {
    let license = { name: this.name.value };
    this.licenseService.create(license as License)
      .subscribe(licenseId => {
        filesPath.forEach(filePath => {
          let licenseFile: LicenseFile;
          licenseFile.path = filePath;
          licenseFile.licenseId = licenseId;
          this.licenseService.createFile(licenseFile).subscribe(_ => _);
        });
      })
      .unsubscribe();
    this.router.navigate(['license/own']);
    this.licenseForm.reset();
  }
}
