import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, AbstractControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { PlaylistService } from '../../../core/services/playlist.service';
import { AuthService } from '../../../core/services/auth.service';
import Playlist from '../../shared/models/playlist';

@Component({
  selector: 'app-playlist-create',
  templateUrl: './playlist-create.component.html',
  styleUrls: ['./playlist-create.component.css']
})
export class PlaylistCreateComponent implements OnInit {
  nameMinLength = 2;
  nameMaxLength = 30;
  playlistForm: FormGroup
  public progress: number;
  public message: string;
  private routeUpload: string = 'song/Upload';
  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    public modal: NgbActiveModal,
    public playlistService: PlaylistService,
    private authService: AuthService) {
    if (authService.isAuth == false) {
      this.router.navigate(['']);
    }
  }

  ngOnInit() {
    this.playlistForm = this.formBuilder.group({
      name: [
        null,
        [
          Validators.required,
          Validators.minLength(this.nameMinLength),
          Validators.maxLength(this.nameMaxLength)
        ]
      ],    
    })
  }

  OnSubmit() {
    let playlist: Playlist = this.playlistForm.value;

    this.playlistService.create(playlist)
      .subscribe(_ => {
        this.modal.close(); //It closes successfully
      })
  }

  get name(): AbstractControl {
    return this.playlistForm.get('name');
  }
}
