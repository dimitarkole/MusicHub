import { Component, HostListener, OnInit } from '@angular/core';
import { CategoryService } from '../../../core/services/category.service';
import { globalConstants } from '../../../common/global-constants';
import { Observable } from 'rxjs';
import Category from '../../shared/models/category';
import { Song } from '../../shared/models/song';
import { FormGroup, FormBuilder, AbstractControl } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { LisenedSongService } from '../../../core/services/lisened-song.service';
import { OrderMethod } from '../../shared/models/OrderMethod';
import { Router } from '@angular/router';
import { ListenedSongPagination } from '../../shared/models/listenedSongPagination';

@Component({
  selector: 'app-listened-song-history-all',
  templateUrl: './listened-song-history-all.component.html',
  styleUrls: ['./listened-song-history-all.component.css']
})
export class ListenedSongHistoryAllComponent implements OnInit {
  listenedSongPagination: ListenedSongPagination = { currentPage: 0, numberOfPages: 1, songViewHistory: [] };
  isFilter: boolean = false;
  categories$: Observable<Category[]>;
  defaultCategoryId: string = 'Each category';
  searchForm: FormGroup;

  constructor(private modalService: NgbModal,
    private lisenedSongService: LisenedSongService,
    private categoryService: CategoryService,
    private router: Router,
    private formBuilder: FormBuilder) {
    this.categories$ = categoryService.all();
  }

  ngOnInit(): void {
    this.getsongViewHistoryPerPage();

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
  }

  submit() {
    this.listenedSongPagination.songViewHistory = [];
    this.listenedSongPagination.currentPage = 0;
    this.getFiltersongViewHistoryPerPage()
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
        this.getsongViewHistoryPerPage();
      }
      else {
        this.getFiltersongViewHistoryPerPage();
      }
    }
  }

  private getFiltersongViewHistoryPerPage(): void {
    var page = this.listenedSongPagination.currentPage + 1;
    if (page <= this.listenedSongPagination.numberOfPages) {
      this.listenedSongPagination.currentPage = page;
      let song: Song = this.createFilterModel();

      this.lisenedSongService.filter(song, page).subscribe(data => {
        this.listenedSongPagination.numberOfPages = data.numberOfPages;
        data.songViewHistory.forEach(song => {
          this.listenedSongPagination.songViewHistory.push(song);
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

  get name(): AbstractControl {
    return this.searchForm.get('name');
  }

  get categoryId(): AbstractControl {
    return this.searchForm.get('categoryId');
  }

  get orderMethod(): AbstractControl {
    return this.searchForm.get('orderMethod');
  }

  private getsongViewHistoryPerPage(): void {
    var page = this.listenedSongPagination.currentPage + 1;
    if (page <= this.listenedSongPagination.numberOfPages) {
      this.listenedSongPagination.currentPage = page;
      this.lisenedSongService.all(page).subscribe(data => {
        console.log(data);
        this.listenedSongPagination.numberOfPages = data.numberOfPages;
        data.songViewHistory.forEach(song => {
          this.listenedSongPagination.songViewHistory.push(song);
        });
      });
    }
  }
}
