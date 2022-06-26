import { Component, Input, OnInit, SimpleChanges } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatDialog, MatDialogConfig } from '@angular/material';
import { ActivatedRoute, Router } from '@angular/router';
import { globalConstants } from '../../../common/global-constants';
import { AuthService } from '../../../core/services/auth.service';
import { MusicService } from '../../services/music.service';
import { Music } from '../../models/music';
import { AddMusicToPlaylistComponent } from '../add-music-to-playlist/add-music-to-playlist.component';
import { MusicDeleteComponent } from '../music-delete/music-delete.component';
import { MusicReportCreateComponent } from '../../../music-report/components/music-report-create/music-report-create.component';

@Component({
  selector: 'app-music-details',
  templateUrl: './music-details.component.html',
  styleUrls: ['./music-details.component.css']
})
export class MusicDetailsComponent implements OnInit {
  isAuth: boolean = false;
  @Input() music: Music;
  musicImageFolder: string = globalConstants.music.imageFolderPath;
  musicAudioFolder: string = globalConstants.music.audioFolderPath;
  musicId: string;
  isAdding: boolean = false;
  isMusicOwned: boolean = false;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    public matDialog: MatDialog,
    private musicSevice: MusicService,
    private route: ActivatedRoute,
    public authService: AuthService) {
    this.isAuth = authService.isAuth;
    this.authService.isAuthChanged.subscribe(() => {
      this.isAuth = this.authService.isAuth;
    });

    this.musicSevice.isOwned(this.musicId).subscribe(r => {
      this.isMusicOwned = r;
    });
  }

  ngOnChanges(changes: SimpleChanges): void {
    this.route.params.subscribe(routeParams => {
      this.preparedPage();
    });
  }

  private preparedPage() {
    this.musicSevice.isOwned(this.musicId).subscribe(r => {
      this.isMusicOwned = r;
    });

    let player = <HTMLAudioElement>document.getElementById("player");
    player.src = this.musicAudioFolder + this.music.audioUrl;
    player.play();
  }

  ngOnInit() {
    this.preparedPage();
  }

  openAddMusic() {
    this.isAdding = !this.isAdding;
  }

  openReportMusic() {
    const dialogConfig = new MatDialogConfig();
    // The user can't close the dialog by clicking outside its body
    dialogConfig.id = "modal-component";
    dialogConfig.width = "600px";
    dialogConfig.data = this.music.id;
    const modalDialog = this.matDialog.open(MusicReportCreateComponent, dialogConfig);
    modalDialog.afterClosed().subscribe(result => {
    });
  }
}
