import { Component, HostListener, Input, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import Album from '../../../albums/models/album';
import { AlbumPagination } from '../../../albums/models/albumPagination';
import { AlbumMusicService } from '../../../albums/services/album-music.service';
import { AlbumService } from '../../../albums/services/album.service';
import { globalConstants } from '../../../common/global-constants';
import { AuthService } from '../../../core/services/auth.service';
import { CreateOrderMethod } from '../../../shared/models/orderMethod';

@Component({
  selector: 'app-add-music-to-albums',
  templateUrl: './add-music-to-albums.component.html',
  styleUrls: ['./add-music-to-albums.component.css']
})
export class AddMusicToAlbumsComponent implements OnInit {
  @Input() musicId: string;
  albumPagination: AlbumPagination = { currentPage: 0, numberOfPages: 1, albums: [] };
  searchForm: FormGroup;
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
    this.searchForm = this.formBuilder.group({
      name: [
        null,
      ],
      orderMethod: [
        "CreatedOnDesc"
      ]
    });

    this.getFilterAlbumsPerPage();
  }

  @HostListener("window:scroll", [])
  onScrollAlbums() {
    var albumMusicsElement = document.getElementById("albums");
    let pos = (albumMusicsElement.scrollTop || albumMusicsElement.scrollTop) + albumMusicsElement.offsetHeight;
    let max = albumMusicsElement.scrollHeight;
    // pos/max will give you the distance between scroll bottom and and bottom of screen in percentage.
    if (pos > max * globalConstants.pagination.updateProcent) {
      //Do your action here
      this.getFilterAlbumsPerPage();
    }
  }

  submit() {
    let album: Album = this.createFilterModel();
    this.albumPagination.currentPage = 1;
    this.albumService.allFilterOwn(1, album).subscribe(data => {
      this.albumPagination.numberOfPages = data.numberOfPages;
      this.albumPagination.albums = data.albums;
    });
  }

  get name(): AbstractControl {
    return this.searchForm.get('name');
  }

  get orderMethod(): AbstractControl {
    return this.searchForm.get('orderMethod');
  }

  private getFilterAlbumsPerPage(): void {
    var page = this.albumPagination.currentPage + 1;
    if (page <= this.albumPagination.numberOfPages) {
      this.albumPagination.currentPage = page;
      let album: Album = this.createFilterModel();
      this.albumService.allFilterOwn(page, album).subscribe(data => {
        this.albumPagination.numberOfPages = data.numberOfPages;
        data.albums.forEach(p => {
          this.albumPagination.albums.push(p);
        });
      });
    }
  }

  private createFilterModel() {
    let album: Album = this.searchForm.value;
    album.orderMethod = CreateOrderMethod(this.orderMethod.value);
    return album;
  }
}
