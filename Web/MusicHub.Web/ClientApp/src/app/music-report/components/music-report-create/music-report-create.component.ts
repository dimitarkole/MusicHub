import { Component, Inject, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { ModalComponent } from 'ngb-modal';
import { globalConstants } from '../../../common/global-constants';
import { MusicService } from '../../../musics/services/music.service';
import ReportMusic from '../../models/musicReport';
import { CreateReportMusicType, ReportMusicType, ReportMusicTypeMapping } from '../../models/musicReportType';
import { MusicReportService } from '../../services/music.report.service';

@Component({
  selector: 'app-music-report-create',
  templateUrl: './music-report-create.component.html',
  styleUrls: ['./music-report-create.component.css']
})
export class MusicReportCreateComponent implements OnInit {
  musicReportForm: FormGroup;
  descriptionMinLength = globalConstants.music.nameMinLength;
  descriptionMaxLength = globalConstants.music.nameMaxLength;
  reportMusicTypeMapping = ReportMusicTypeMapping;
  reportMusicTypes = Object.keys(ReportMusicType)
    .filter(k => !isNaN(Number(k)));;
  constructor(public dialogRef: MatDialogRef<ModalComponent>,
    private formBuilder: FormBuilder,
    private musicReportService: MusicReportService,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private musicService: MusicService) {
  }

  ngOnInit() {
    this.musicReportForm = this.formBuilder.group({
      musicId: [
        this.data,
        [
          Validators.required,
        ]
      ],
      description: [
        "",
        [
          Validators.required,
        ]
      ],
      type: [
        ReportMusicType.NoType,
        [
          Validators.required,
          Validators.pattern('^((?!default).)*$'),
        ]
      ],
    });
  }

  // If the user clicks the cancel button a.k.a. the go back button, then\
  // just close the modal
  closeModal() {
    this.dialogRef.close();
  }

  OnSubmit() {
    let musicReport: ReportMusic = this.musicReportForm.value;
    musicReport.type = CreateReportMusicType(musicReport.type);
    console.log(musicReport);
    this.musicReportService.create(musicReport)
      .subscribe(_ => {
      });
  }

  get description(): AbstractControl {
    return this.musicReportForm.get('description');
  }

  get type(): AbstractControl {
    return this.musicReportForm.get('type');
  }
}
