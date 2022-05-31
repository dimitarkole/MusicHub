import { Component, Input, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import Album from '../../../albums/models/album';
import AlbumMusic from '../../../albums/models/albumMusic';
import { AlbumMusicService } from '../../../albums/services/album-music.service';
import { AlbumService } from '../../../albums/services/album.service';
import { AuthService } from '../../../core/services/auth.service';

@Component({
  selector: 'app-add-music-to-album',
  templateUrl: './add-music-to-album.component.html',
  styleUrls: ['./add-music-to-album.component.css']
})
export class AddMusicToAlbumComponent implements OnInit {
  @Input() album: Album;
  @Input() musicId: string;
  albumMusicForm: FormGroup
  isChecked: boolean = false;
  isSuccess: boolean = false;

  public progress: number;
  public message: string;
  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    public modal: NgbActiveModal,
    public albumMusicsService: AlbumMusicService,
    public albumService: AlbumService,
    private authService: AuthService) {
    if (authService.isAuth == false) {
      this.router.navigate(['']);
    }
  }

  ngOnInit() {
    this.albumMusicForm = this.formBuilder.group({
      albumId: [
        this.album.id,
        [
          Validators.required,
          Validators.pattern('^((?!default).)*$'),
        ]
      ],
      musicId: this.musicId,
    });

    var albumMusic: AlbumMusic = { albumId: this.album.id, musicId: this.musicId };
    this.albumMusicsService.isAddedToAlbum(albumMusic).subscribe(r => this.isChecked = r);
  }

  checkValue(event: any) {
    if (event.currentTarget.checked) {
      // add music to album
      let addMusicToAlbum: AlbumMusic = this.albumMusicForm.value;
      this.albumMusicsService.create(addMusicToAlbum)
        .subscribe(_ => this.successfull());
    }
    else {
      // remove music from album
      let addMusicToAlbum: AlbumMusic = this.albumMusicForm.value;
      this.albumMusicsService.getAlbumMusicId(addMusicToAlbum)
        .subscribe(r => {
          this.albumMusicsService.delete(r.id).subscribe(_ => _);
          this.successfull();
        })
    }
  }

  successfull() {
    this.isSuccess = true;
    setTimeout(() => { this.isSuccess = false }, 5000);
  }

  wait(ms) {
    var start = new Date().getTime();
    var end = start;
    while (end < start + ms) {
      end = new Date().getTime();
    }
  }

  get albumId(): AbstractControl {
    return this.albumMusicForm.get('albumId');
  }
}
