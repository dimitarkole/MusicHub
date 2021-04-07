import { Component, HostListener, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Observable } from 'rxjs';
import Category from '../../../categories/models/category';
import { CategoryService } from '../../../categories/services/category.service';
import { globalConstants } from '../../../common/global-constants';
import { CreateOrderMethod } from '../../../shared/models/orderMethod';
import { Music } from '../../models/music';
import { MusicPagination } from '../../models/musicPagination';
import { MusicService } from '../../services/music.service';

@Component({
  selector: 'app-musics-own-list',
  templateUrl: './musics-own-list.component.html',
  styleUrls: ['./musics-own-list.component.css']
})
export class MusicsOwnListComponent implements OnInit {
  musicPagination: MusicPagination = { currentPage: 0, numberOfPages: 1, musics: [] };
  isFilter: boolean = false;
  categories$: Observable<Category[]>;
  defaultCategoryId: string = 'Each category';
  searchForm: FormGroup;

  constructor(private modalService: NgbModal,
    private musicService: MusicService,
    private categoryService: CategoryService,
    private router: Router,
    private formBuilder: FormBuilder) {
    this.musicPagination.musics = new Array<Music>();
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
    this.getMusicsPerPage();
  }

  submit() {
    let music: Music = this.createFilterModel();
    this.musicPagination.currentPage = 1;
    this.isFilter = true;
    this.musicService.allOwnFilter(1, music).subscribe(data => {
      this.musicPagination.numberOfPages = data.numberOfPages;
      this.musicPagination.musics = data.musics;
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
        this.getMusicsPerPage();
      }
      else {
        this.getFilterMusicsPerPage();
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

  private getMusicsPerPage(): void {
    var page = this.musicPagination.currentPage + 1;
    if (page <= this.musicPagination.numberOfPages) {
      this.musicPagination.currentPage = page;
      this.musicService.allOwn(page).subscribe(data => {
        this.musicPagination.numberOfPages = data.numberOfPages;
        data.musics.forEach(song => {
          this.musicPagination.musics.push(song);
        });
      });
    }
  }

  private getFilterMusicsPerPage(): void {
    var page = this.musicPagination.currentPage + 1;
    if (page <= this.musicPagination.numberOfPages) {
      this.musicPagination.currentPage = page;
      let music: Music = this.createFilterModel();

      this.musicService.allOwnFilter(page, music).subscribe(data => {
        this.musicPagination.numberOfPages = data.numberOfPages;
        data.musics.forEach(song => {
          this.musicPagination.musics.push(song);
        });
      });
    }
  }

  private createFilterModel() {
    let music: Music = this.searchForm.value;
    music.orderMethod = CreateOrderMethod(this.orderMethod.value);
    if (music.categoryId == this.defaultCategoryId) {
      music.categoryId = null;
    }
    return music;
  }
}
