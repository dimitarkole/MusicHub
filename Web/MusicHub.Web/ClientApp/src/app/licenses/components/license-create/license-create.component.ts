import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { globalConstants } from '../../../common/global-constants';
import { modelConstants } from '../../../common/model-constants';
import { MegaStorageFileService } from '../../../core/services/mega-storage-file.service';
import License from '../../models/license';
import LicenseFile from '../../models/licenseFile';
import { LicenseService } from '../../services/license.service';

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
    private fileService: MegaStorageFileService,
    private licenseService: LicenseService) {
    this.imageUrls = [];
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
