import { Component, HostListener, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup } from '@angular/forms';
import { MatDialog } from '@angular/material';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Observable } from 'rxjs';
import Category from '../../../categories/models/category';
import { CategoryService } from '../../../categories/services/category.service';
import { globalConstants } from '../../../common/global-constants';
import { BooleanSearchService } from '../../../core/resolvers/boolean-search.service';
import { AuthService } from '../../../core/services/auth.service';
import { BooleanSearchEntity } from '../../../shared/models/boolean-search';
import { ReactionInfo } from '../../../shared/models/reactionInfo';
import { LikedMusicPagination } from '../../models/likedMusicPagination';
import { Music } from '../../models/music';
import { PlayedMusic } from '../../models/playedMusic';
import { PlayedMusicPagination } from '../../models/PlayedMusicPagination';
import { MusicReactionService } from '../../services/music-reaction.service';
import { MusicService } from '../../services/music.service';
import { PlayedMusicService } from '../../services/played-music.service';

@Component({
  selector: 'app-played-musics-lists',
  templateUrl: './played-musics-lists.component.html',
  styleUrls: ['./played-musics-lists.component.css']
})
export class PlayedMusicsListsComponent implements OnInit {

  playedMusicPagination: PlayedMusicPagination = { currentPage: 0, numberOfPages: 1, playedMusics: [] };
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
    private playedMusicService: PlayedMusicService,
    private categoryService: CategoryService,
    private searchService: BooleanSearchService,
    private route: ActivatedRoute,
    private formBuilder: FormBuilder) {
    this.playedMusicPagination.playedMusics = new Array<PlayedMusic>();
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
    if (this.playedMusicPagination.currentPage < this.playedMusicPagination.numberOfPages) {
      this.route.queryParams.subscribe(queryParams => {
        let query = queryParams.searchQuery;
        if (!query) {
          if (this.isSearching == true) {
            this.ResetData(query);
            this.isSearching = false;
          }

          var page = this.playedMusicPagination.currentPage + 1;
          this.playedMusicService.all(page).subscribe(data => {
            this.playedMusicPagination.numberOfPages = data.numberOfPages;
            data.playedMusics.forEach(music => {
              this.playedMusicPagination.playedMusics.push(music);
            });
          });
        }
        else {
          this.isSearching = true;
          if (this.search != query) {
            this.ResetData(query);
          }

          var page = this.playedMusicPagination.currentPage + 1;
          this.searchService.search<PlayedMusicPagination>({ query }, BooleanSearchEntity.PlayedMusics, page).subscribe(data => {
            this.playedMusicPagination.numberOfPages = data.numberOfPages
            data.playedMusics.forEach(music => {
              this.playedMusicPagination.playedMusics.push(music);
            });
          });
        }

        this.playedMusicPagination.currentPage = page;
      });
    }
  }

  private ResetData(query: any) {
    this.search = query;
    this.playedMusicPagination = { currentPage: 0, numberOfPages: 1, playedMusics: [] };
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
