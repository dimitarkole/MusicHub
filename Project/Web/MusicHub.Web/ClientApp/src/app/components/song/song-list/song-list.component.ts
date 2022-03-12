import { Component, OnInit, Input, OnChanges, SimpleChanges, HostListener } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { FormGroup, FormBuilder, AbstractControl } from '@angular/forms';
import Category from '../../shared/models/category';
import { globalConstants } from '../../../common/global-constants';
import { Song } from '../../shared/models/song';
import { CategoryService } from '../../../core/services/category.service';
import { SongService } from '../../../core/services/song.service';
import { AuthService } from '../../../core/services/auth.service';
import { OrderMethod } from '../../shared/models/OrderMethod';
import { SongPagination } from '../../shared/models/songPagination';

@Component({
  selector: 'app-song-list',
  templateUrl: './song-list.component.html',
  styleUrls: ['./song-list.component.css']
})
export class SongListComponent implements OnInit {
  songPagination: SongPagination = { currentPage: 0, numberOfPages: 1, songs: [] };
  isFilter: boolean = false;
  categories$: Observable<Category[]>;
  defaultCategoryId: string = 'Each category';
  searchForm: FormGroup;

  constructor(private modalService: NgbModal,
    private songService: SongService,
    private categoryService: CategoryService,
    private router: Router,
    private formBuilder: FormBuilder,
    private authService: AuthService) {
    this.categories$ = categoryService.all();
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
    this.isFilter = true;
    this.songPagination.currentPage = 1;
    this.songService.allOwnFilter(1, song).subscribe(data => {
      this.songPagination.numberOfPages = data.numberOfPages;
      this.songPagination.songs = data.songs;
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

  private getSongsPerPage(): void {
    var page = this.songPagination.currentPage + 1;
    if (page <= this.songPagination.numberOfPages) {
      this.songPagination.currentPage = page;
      this.songService.allOwn(page).subscribe(data => {
        this.songPagination.numberOfPages = data.numberOfPages;
        data.songs.forEach(song => {
          this.songPagination.songs.push(song);
        });
        console.log(data);
      });
    }
  }

  private getFilterSongsPerPage(): void {
    var page = this.songPagination.currentPage + 1;
    if (page <= this.songPagination.numberOfPages) {
      this.songPagination.currentPage = page;
      let song: Song = this.createFilterModel();

      this.songService.allOwnFilter(page, song).subscribe(data => {
        this.songPagination.numberOfPages = data.numberOfPages;
        data.songs.forEach(song => {
          this.songPagination.songs.push(song);
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
