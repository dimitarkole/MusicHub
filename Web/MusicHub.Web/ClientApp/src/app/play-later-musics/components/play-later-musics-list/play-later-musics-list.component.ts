import { Component, HostListener, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup } from '@angular/forms';
import { MatDialog, MatDialogConfig } from '@angular/material';
import { ActivatedRoute } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Observable } from 'rxjs';
import Category from '../../../categories/models/category';
import { CategoryService } from '../../../categories/services/category.service';
import { globalConstants } from '../../../common/global-constants';
import { BooleanSearchService } from '../../../core/resolvers/boolean-search.service';
import { BooleanSearchEntity } from '../../../shared/models/boolean-search';
import { PlayLaterMusic } from '../../models/playLaterMusic';
import { PlayLaterMusicPagination } from '../../models/playLaterMusicPagination';
import { PlayLaterMusicService } from '../../services/play-later-music.service';
import { PlayLaterMusicDeleteComponent } from '../play-later-music-delete/play-later-music-delete.component';

@Component({
  selector: 'app-play-later-musics-list',
  templateUrl: './play-later-musics-list.component.html',
  styleUrls: ['./play-later-musics-list.component.css']
})
export class PlayLaterMusicsListComponent implements OnInit {
  playLaterMusicsPagination: PlayLaterMusicPagination = { currentPage: 0, numberOfPages: 1, playLaterMusics: [] };
  isFilter: boolean = false;
  categories$: Observable<Category[]>;
  defaultCategoryId: string = 'Each category';
  search: string = "";
  isSearching: boolean = false;
  searchForm: FormGroup;
  musicAudioPath: string = globalConstants.music.audioFolderPath;
  musicFolderPath: string = globalConstants.music.imageFolderPathToUpload;
  defauleMusicImagePath: string = this.musicFolderPath + globalConstants.music.defauldImage;

  constructor(private modalService: NgbModal,
    private categoryService: CategoryService,
    private searchService: BooleanSearchService,
    private playLaterMusicService: PlayLaterMusicService,
    public matDialog: MatDialog,
    private route: ActivatedRoute,
    private formBuilder: FormBuilder) {
    this.playLaterMusicsPagination.playLaterMusics = new Array<PlayLaterMusic>();
    this.categories$ = categoryService.all();
  }

  ngOnInit(): void {
    this.searchForm = this.formBuilder.group({
      name: [
        null,
      ],
      categoryId: [
        this.defaultCategoryId
      ],
      orderMethod: [
        "CreatedOnDesc"
      ]
    });
    this.feachData();
  }

  @HostListener("window:scroll", [])
  onWindowScroll() {
    //In chrome and some browser scroll is given to body tag
    let pos = (document.documentElement.scrollTop || document.body.scrollTop) + document.documentElement.offsetHeight;
    let max = document.documentElement.scrollHeight;
    // pos/max will give you the distance between scroll bottom and and bottom of screen in percentage.
    if (pos > max * globalConstants.pagination.updateProcent) {
      //Do your action here
      this.feachData();
    }
  }

  private feachData(): void {
    if (this.playLaterMusicsPagination.currentPage < this.playLaterMusicsPagination.numberOfPages) {
      this.route.queryParams.subscribe(queryParams => {
        let query = queryParams.searchQuery;
        if (!query) {
          if (this.isSearching == true) {
            this.ResetData(query);
            this.isSearching = false;
          }

          var page = this.playLaterMusicsPagination.currentPage + 1;
          this.playLaterMusicService.all(page).subscribe(data => {
            this.playLaterMusicsPagination.numberOfPages = data.numberOfPages;
            data.playLaterMusics.forEach(music => {
              this.playLaterMusicsPagination.playLaterMusics.push(music);
            });
          });
        }
        else {
          this.isSearching = true;
          if (this.search != query) {
            this.ResetData(query);
          }

          var page = this.playLaterMusicsPagination.currentPage + 1;
          this.searchService.search<PlayLaterMusicPagination>({ query }, BooleanSearchEntity.LikedMusics, page).subscribe(data => {
            this.playLaterMusicsPagination.numberOfPages = data.numberOfPages
            data.playLaterMusics.forEach(music => {
              this.playLaterMusicsPagination.playLaterMusics.push(music);
            });
          });
        }

        this.playLaterMusicsPagination.currentPage = page;
      });
    }
  }

  private ResetData(query: any) {
    this.search = query;
    this.playLaterMusicsPagination = { currentPage: 0, numberOfPages: 1, playLaterMusics: [] };
  }

  openDelete(playLaterMusicId: string) {
    const dialogConfig = new MatDialogConfig();
    // The user can't close the dialog by clicking outside its body
    dialogConfig.id = "modal-component";
    dialogConfig.width = "600px";
    dialogConfig.data = playLaterMusicId;
    // https://material.angular.io/components/dialog/overview
    const modalDialog = this.matDialog.open(PlayLaterMusicDeleteComponent, dialogConfig);
    modalDialog.afterClosed().subscribe(result => {
       if (result == true) window.location.reload();
    });
  }

  get name(): AbstractControl {
    return this.searchForm.get('name');
  }

  get categoryId(): AbstractControl {
    return this.searchForm.get('categoryId');
  }

  get orderMethod(): AbstractControl {
    return this.searchForm.get('orderMethod');
  }
}
