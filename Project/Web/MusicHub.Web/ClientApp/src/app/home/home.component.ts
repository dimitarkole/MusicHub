import { Component, HostListener, Input, OnInit } from '@angular/core';
import { Song } from '../components/shared/models/song';
import { globalConstants } from '../common/global-constants';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { SongService } from '../core/services/song.service';
import { Router } from '@angular/router';
import getPage from '../common/paginator';
import { FormBuilder, FormGroup, Validators, AbstractControl } from '@angular/forms';
import { Observable } from 'rxjs';
import Category from '../components/shared/models/category';
import { CategoryService } from '../core/services/category.service';
import { OrderMethod } from '../components/shared/models/OrderMethod';
import { SongPagination } from '../components/shared/models/songPagination';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  songPagination: SongPagination = { currentPage: 0, numberOfPages: 1, songs: [] };
  isFilter: boolean = false;
  categories$: Observable<Category[]>;
  defaultCategoryId: string = 'Each category';
  searchForm: FormGroup;

  constructor(private modalService: NgbModal,
    private songService: SongService,
    private categoryService: CategoryService,
    private router: Router,
    private formBuilder: FormBuilder) {
    this.songPagination.songs = new Array<Song>();
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
    this.songPagination.currentPage = 1;
    this.isFilter = true;
    this.songService.allFilter(1, song).subscribe(data => {
      this.songPagination.numberOfPages = data.numberOfPages;
      this.songPagination.songs = data.songs;
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
    var page = this.songPagination.currentPage + 1;
    if (page <= this.songPagination.numberOfPages) {
      this.songPagination.currentPage = page;
      this.songService.all(page).subscribe(data => {
        this.songPagination.numberOfPages = data.numberOfPages;
        data.songs.forEach(song => {
          this.songPagination.songs.push(song);
        });
      });
    }
  }

  private getFilterSongsPerPage(): void {
    var page = this.songPagination.currentPage + 1;
    if (page <= this.songPagination.numberOfPages) {
      this.songPagination.currentPage = page;
      let song: Song = this.createFilterModel();

      this.songService.allFilter(page, song).subscribe(data => {
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
