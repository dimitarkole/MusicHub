import { Component, HostListener, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Observable } from 'rxjs';
import { globalConstants } from '../../../common/global-constants';
import { CategoryService } from '../../../core/services/category.service';
import { SongReactionService } from '../../../core/services/song-reaction.service';
import Category from '../../shared/models/category';
import { LikedSongPagination } from '../../shared/models/likedSongPagination';
import { OrderMethod } from '../../shared/models/OrderMethod';
import { ReactionInfo } from '../../shared/models/reactionInfo';
import { Song } from '../../shared/models/song';

@Component({
  selector: 'app-liked-songs-list',
  templateUrl: './liked-songs-list.component.html',
  styleUrls: ['./liked-songs-list.component.css']
})

export class LikedSongsListComponent implements OnInit {
  likedSongPagination: LikedSongPagination = { currentPage: 0, numberOfPages: 1, linkedSongs: [] };
  isFilter: boolean = false;
  categories$: Observable<Category[]>;
  defaultCategoryId: string = 'Each category';
  searchForm: FormGroup;

  constructor(private modalService: NgbModal,
    private reactionService: SongReactionService,
    private categoryService: CategoryService,
    private router: Router,
    private formBuilder: FormBuilder) {
    this.likedSongPagination.linkedSongs = new Array<ReactionInfo>();
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
    this.getSongsPerPage();  
  }

  submit() {
    let song: Song = this.createFilterModel();
    this.likedSongPagination.currentPage = 1;
    this.isFilter = true;
    this.reactionService.filterLikedSongs(1, song).subscribe(data => {
      this.likedSongPagination.numberOfPages = data.numberOfPages;
      this.likedSongPagination.linkedSongs = data.linkedSongs;
    });
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
        this.getSongsPerPage();
      }
      else {
        this.getFilterSongsPerPage();
      }
    }
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

  private getSongsPerPage(): void {
    var page = this.likedSongPagination.currentPage + 1;
    if (page <= this.likedSongPagination.numberOfPages) {
      this.likedSongPagination.currentPage = page;
      this.reactionService.allLikedSongs(page).subscribe(data => {
        this.likedSongPagination.numberOfPages = data.numberOfPages;
        data.linkedSongs.forEach(song => {
          this.likedSongPagination.linkedSongs.push(song);
        });
      });
    }
  }

  private getFilterSongsPerPage(): void {
    var page = this.likedSongPagination.currentPage + 1;
    if (page <= this.likedSongPagination.numberOfPages) {
      this.likedSongPagination.currentPage = page;
      let song: Song = this.createFilterModel();

      this.reactionService.filterLikedSongs(page, song).subscribe(data => {
        this.likedSongPagination.numberOfPages = data.numberOfPages;
        data.linkedSongs.forEach(song => {
          this.likedSongPagination.linkedSongs.push(song);
        });
      });
    }
  }

  private createFilterModel() {
    let song: Song = this.searchForm.value;
    switch (this.orderMethod.value) {
      case "CreatedOnDesc": {
        song.orderMethod = OrderMethod.CreatedOnDesc;
        break;
      }
      case "NameAsc": {
        song.orderMethod = OrderMethod.NameAsc;
        break;
      }
      case "NameDesc": {
        song.orderMethod = OrderMethod.NameDesc;
        break;
      }
      default: {
        song.orderMethod = OrderMethod.CreatedOnAsc;
        break;
      }
    }
    if (song.categoryId == this.defaultCategoryId) {
      song.categoryId = null;
    }
    return song;
  }

}
