import { Component, HostListener, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Observable } from 'rxjs';
import Category from '../../../categories/models/category';
import { CategoryService } from '../../../categories/services/category.service';
import { globalConstants } from '../../../common/global-constants';
import { BooleanSearchService } from '../../../core/resolvers/boolean-search.service';
import { BooleanSearchEntity } from '../../../shared/models/boolean-search';
import { OrderMethod } from '../../../shared/models/orderMethod';
import { ReactionInfo } from '../../../shared/models/reactionInfo';
import { LikedMusicPagination } from '../../models/likedMusicPagination';
import { Music } from '../../models/music';
import { MusicReactionService } from '../../services/music-reaction.service';

@Component({
  selector: 'app-liked-musics-list',
  templateUrl: './liked-musics-list.component.html',
  styleUrls: ['./liked-musics-list.component.css']
})
export class LikedMusicsListComponent implements OnInit {
  likedMusicsPagination: LikedMusicPagination = { currentPage: 0, numberOfPages: 1, likedMusics: [] };
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
    private musicReactionService: MusicReactionService,
    private categoryService: CategoryService,
    private searchService: BooleanSearchService,
    private route: ActivatedRoute,
    private formBuilder: FormBuilder) {
    this.likedMusicsPagination.likedMusics = new Array<ReactionInfo>();
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
    if (this.likedMusicsPagination.currentPage < this.likedMusicsPagination.numberOfPages) {
      this.route.queryParams.subscribe(queryParams => {
        let query = queryParams.searchQuery;
        if (!query) {
          if (this.isSearching == true) {
            this.ResetData(query);
            this.isSearching = false;
          }

          var page = this.likedMusicsPagination.currentPage + 1;
          this.musicReactionService.allLikedMusic(page).subscribe(data => {
            this.likedMusicsPagination.numberOfPages = data.numberOfPages;
            data.likedMusics.forEach(music => {
              this.likedMusicsPagination.likedMusics.push(music);
            });
          });
        }
        else {
          this.isSearching = true;
          if (this.search != query) {
            this.ResetData(query);
          }

          var page = this.likedMusicsPagination.currentPage + 1;
          this.searchService.search<LikedMusicPagination>({ query }, BooleanSearchEntity.LikedMusics, page).subscribe(data => {
            this.likedMusicsPagination.numberOfPages = data.numberOfPages
            data.likedMusics.forEach(music => {
              this.likedMusicsPagination.likedMusics.push(music);
            });
          });
        }

        this.likedMusicsPagination.currentPage = page;
      });
    }
  }

  private ResetData(query: any) {
    this.search = query;
    this.likedMusicsPagination = { currentPage: 0, numberOfPages: 1, likedMusics: [] };
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
