import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatDialog, MatDialogConfig } from '@angular/material';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { globalConstants } from '../../../common/global-constants';
import { AuthService } from '../../../core/services/auth.service';
import { Music } from '../../models/music';
import { MusicService } from '../../services/music.service';
import { MusicDeleteComponent } from '../music-delete/music-delete.component';

@Component({
  selector: 'app-musics-list-template',
  templateUrl: './musics-list-template.component.html',
  styleUrls: ['./musics-list-template.component.css']
})
export class MusicsListTemplateComponent implements OnInit {
  @Input() musics: Music[] = [];
  @Input() type: string;
 // @Input() type: string;
  musicAudioPath: string = globalConstants.MusicAudioPath;
  musicImagePath: string = globalConstants.MusicImagePath;
  deleteForm: FormGroup;

  constructor(private modalService: NgbModal,
    private musicService: MusicService,
    private router: Router,
    public matDialog: MatDialog,
    //private fileService: FileService,
    private formBuilder: FormBuilder,
    private authService: AuthService) {
  }

  ngOnInit() {
    this.deleteForm = this.formBuilder.group({
      path: null,
      fileName: null,
    });
  }

  openEdit(music: Music) {
    this.router.navigate(['/musics/edit', music.id]);
  }

  openDelete(musicId: string) {
    const dialogConfig = new MatDialogConfig();
    // The user can't close the dialog by clicking outside its body
    dialogConfig.id = "modal-component";
    dialogConfig.width = "600px";
    dialogConfig.data = musicId;
    // https://material.angular.io/components/dialog/overview
    const modalDialog = this.matDialog.open(MusicDeleteComponent, dialogConfig);
    modalDialog.afterClosed().subscribe(result => {
      if (result == true) window.location.reload();
    });
  }
}
