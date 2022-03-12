import { Component, OnInit, Input, OnChanges, SimpleChanges } from '@angular/core';
import { globalConstants } from '../../../common/global-constants';
import Playlist from '../../shared/models/playlist';
import getPage from '../../../common/paginator';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { PlaylistService } from '../../../core/services/playlist.service';
import { FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';
import { PlaylistEditComponent } from '../playlist-edit/playlist-edit.component';
import { PlaylistDeleteComponent } from '../playlist-delete/playlist-delete.component';

@Component({
  selector: 'app-playlist-list-template',
  templateUrl: './playlist-list-template.component.html',
  styleUrls: ['./playlist-list-template.component.css']
})
export class PlaylistListTemplateComponent implements OnInit, OnChanges {
  @Input() playlists: Playlist[] = [];
  @Input() type: string;

  constructor(private modalService: NgbModal,
    private playlistService: PlaylistService,
    private formBuilder: FormBuilder,
    private router: Router,
    private authService: AuthService) { }

  ngOnChanges(changes: SimpleChanges): void {
  }

  ngOnInit() {
  }

  openEdit(playlist: Playlist) {
    let modal = this.modalService.open(PlaylistEditComponent);

    modal.componentInstance.playlist = playlist;
    modal.result.then(_ => {
      this.router.navigate(['/playlist/own']);
    }).catch(err => {
      console.log(err);
    })
  }

  openDelete(playlistId: string) {
    let modal = this.modalService.open(PlaylistDeleteComponent);

    modal.result.then(value => {
      this.playlistService.delete(playlistId).subscribe(_ => {
        this.router.navigate(['/playlist/own']);
      })
    }).catch(err => {
      console.log(err);
    })
  }
}
