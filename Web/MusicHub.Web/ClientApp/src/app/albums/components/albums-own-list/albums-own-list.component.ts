import { Component, HostListener, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup } from '@angular/forms';
import { MatDialog, MatDialogConfig } from '@angular/material';
import { Router } from '@angular/router';
import { globalConstants } from '../../../common/global-constants';
import { CreateOrderMethod } from '../../../shared/models/orderMethod';
import Album from '../../models/album';
import { AlbumPagination } from '../../models/albumPagination';
import { AlbumService } from '../../services/album.service';
import { AlbumCreateComponent } from '../album-create/album-create.component';

@Component({
  selector: 'app-albums-own-list',
  templateUrl: './albums-own-list.component.html',
  styleUrls: ['./albums-own-list.component.css']
})
export class AlbumsOwnListComponent implements OnInit {
  albumPagination: AlbumPagination = { currentPage: 0, numberOfPages: 1, albums: [] };
  isFilter: boolean = false;
  searchForm: FormGroup;

  constructor(public matDialog: MatDialog,
    private albumService: AlbumService,
    private formBuilder: FormBuilder,
    private router: Router) {
  }

  ngOnInit(): void {
    this.searchForm = this.formBuilder.group({
      name: [
        null,
      ],
      orderMethod: [
        "CreatedOnDesc"
      ]
    });
    this.getAlbumsPerPage();
  }

  @HostListener("window:scroll", [])
  onWindowScroll() {
    //In chrome and some browser scroll is given to body tag
    let pos = (document.documentElement.scrollTop || document.body.scrollTop) + document.documentElement.offsetHeight;
    let max = document.documentElement.scrollHeight;
    // pos/max will give you the distance between scroll bottom and and bottom of screen in percentage.
    if (pos > max * globalConstants.pagination.updateProcent) {
      //Do your action here
      if (this.isFilter == false) {
        this.getAlbumsPerPage();
      }
      else {
        this.getFilterAlbumsPerPage();
      }
    }
  }

  submit() {
    let album: Album = this.createFilterModel();
    this.isFilter = true;
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

  openCreate() {
    const dialogConfig = new MatDialogConfig();
    // The user can't close the dialog by clicking outside its body
    dialogConfig.id = "modal-component";
    dialogConfig.width = "800px";
    // https://material.angular.io/components/dialog/overview
    const modalDialog = this.matDialog.open(AlbumCreateComponent, dialogConfig);
    modalDialog.afterClosed().subscribe(result => {
      if (result == true) window.location.reload();
    });
  }

  private getAlbumsPerPage(): void {
    var page = this.albumPagination.currentPage + 1;
    if (page <= this.albumPagination.numberOfPages) {
      this.albumPagination.currentPage = page;
      this.albumService.allOwn(page).subscribe(data => {
        this.albumPagination.numberOfPages = data.numberOfPages;
        data.albums.forEach(song => {
          this.albumPagination.albums.push(song);
        });
      });
    }
  }

  private getFilterAlbumsPerPage(): void {
    var page = this.albumPagination.currentPage + 1;
    if (page <= this.albumPagination.numberOfPages) {
      this.albumPagination.currentPage = page;
      let album: Album = this.createFilterModel();

      this.albumService.allFilterOwn(page, album).subscribe(data => {
        this.albumPagination.numberOfPages = data.numberOfPages;
        data.albums.forEach(song => {
          this.albumPagination.albums.push(song);
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
