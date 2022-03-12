import { Component, OnInit, Input, OnChanges, SimpleChanges } from '@angular/core';
import { Song } from '../../shared/models/song';
import { globalConstants } from '../../../common/global-constants';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { CategoryService } from '../../../core/services/category.service';
import { SongService } from '../../../core/services/song.service';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup } from '@angular/forms';
import { AuthService } from '../../../core/services/auth.service';
import getPage from '../../../common/paginator';
import { SongDeleteModalComponent } from '../song-delete-modal/song-delete-modal.component';
import { FileService } from '../../../core/services/file.service';
import File from '../../shared/models/file';
import FileInfo from '../../shared/models/file';

@Component({
  selector: 'app-song-list-template',
  templateUrl: './song-list-template.component.html',
  styleUrls: ['./song-list-template.component.css']
})
export class SongListTemplateComponent implements OnInit {
  @Input() songs: Song[] = [];
  @Input() type: string;
  songAudioPath: string = globalConstants.songAudioPath;
  songImagePath: string = globalConstants.songImagePath;
  deleteForm: FormGroup;

  constructor(private modalService: NgbModal,
    private songService: SongService,
    private router: Router,
    private fileService: FileService,
    private formBuilder: FormBuilder,
    private authService: AuthService) {
    if (authService.isAuth == false) {
    }
  }

  ngOnInit() {
    this.deleteForm = this.formBuilder.group({
      path: null,
      fileName: null,
    });
  }

  openEdit(song: Song) {
    this.router.navigate(['/song/edit', song.id]);
  }

  openDelete(songId: string) {
    let modal = this.modalService.open(SongDeleteModalComponent);
    modal.result.then(value => {
      this.songService.getById(songId).subscribe(songData => {
        this.deleteForm.get('fileName').setValue(songData.audioFilePath);
        this.deleteForm.get('path').setValue(globalConstants.songAudioPath);
        let audioFile: FileInfo = this.deleteForm.value;
       
        this.fileService.deleteFile(audioFile).subscribe(_ => {
          if (songData.imageFilePath != globalConstants.defaultImage) {
            this.deleteForm.get('fileName').setValue(songData.imageFilePath);
            this.deleteForm.get('path').setValue(globalConstants.songImagePath);
            let imageFile: FileInfo = this.deleteForm.value;
            this.fileService.deleteFile(imageFile).subscribe(_ => {
              this.deleteSong(songId);
            });
          }
          else {
            this.deleteForm.get('path').setValue(globalConstants.songImagePath);
            let imageFile: FileInfo = this.deleteForm.value;
            this.fileService.deleteFile(imageFile).subscribe(_ => {
              this.deleteSong(songId);
            });
          }
        });
      });
    }).catch(err => {
      console.log(err);
    })
  }

  private deleteSong(songId: string) {
    this.songService.delete(songId).subscribe(_ => {
      this.router.navigate(['/song/own']);
    })
  }
}
