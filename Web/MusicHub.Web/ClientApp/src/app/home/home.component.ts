import { Component, HostListener, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { globalConstants } from '../common/global-constants';
import { NgbActiveModal, NgbModal, NgbModalConfig } from '@ng-bootstrap/ng-bootstrap';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators, AbstractControl } from '@angular/forms';
import { Observable } from 'rxjs';
import { CategoryCreateComponent } from '../categories/components/category-create/category-create.component';
import { CategoryDeleteComponent } from '../categories/components/category-delete/category-delete.component';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { MusicPagination } from '../musics/models/musicPagination';
import Category from '../categories/models/category';
import { MusicService } from '../musics/services/music.service';
import { CategoryService } from '../categories/services/category.service';
import { Music } from '../musics/models/music';
import { CreateOrderMethod, OrderMethod } from '../shared/models/orderMethod';
import { BooleanSearchEntity } from '../shared/models/boolean-search';
import { cwd } from 'process';
import { BooleanSearchService } from '../core/resolvers/boolean-search.service';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
    // add NgbModalConfig and NgbModal to the component providers
})

export class HomeComponent implements OnInit {
  musicPagination: MusicPagination = { currentPage: 0, numberOfPages: 1, musics: [] };
  isFilter: boolean = false;
  categories$: Observable<Category[]>;
  defaultCategoryId: string = 'Each category';
  search: string = "";
  isSearching: boolean = false;
  
  constructor(private modalService: NgbModal,
    private router: Router,
    private musicService: MusicService,
    private categoryService: CategoryService,
		private searchService: BooleanSearchService,
    private route: ActivatedRoute,
    private formBuilder: FormBuilder) {
    this.musicPagination.musics = new Array<Music>();
    this.categories$ = categoryService.all();
  }

  ngOnInit(): void {
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
    if (this.musicPagination.currentPage < this.musicPagination.numberOfPages) {
      this.route.queryParams.subscribe(queryParams => {
        var musicFilter: Music = {
          name: queryParams.searchQuery == undefined? "": queryParams.searchQuery,
          categoryId: queryParams.categoryId == undefined ? "" :queryParams.categoryId,
        };
        var page = this.musicPagination.currentPage + 1;
        this.musicService.allFilter(page, musicFilter).subscribe(data => {
          this.musicPagination.numberOfPages = data.numberOfPages;
          data.musics.forEach(music => {
            this.musicPagination.musics.push(music);
          });
        });
        this.musicPagination.currentPage = page;
      });
    }
  }

  private ResetData(query: any) {
    this.search = query;
    this.musicPagination = { currentPage: 0, numberOfPages: 1, musics: [] };
  }

  filterByCategory(categoryId: string) {
    var searchValue = "", type="";
    this.route.queryParams.subscribe(params => {
      searchValue = params['searchQuery'];
      type = params['type'];
    });
    
    this.router.navigate([""], { queryParams: { searchQuery: searchValue, type: type, categoryId: categoryId } })
      .then(() => {
        this.musicPagination.musics = [];
        this.musicPagination.currentPage = 0;
        this.feachData();
      });
  }

  selectedOrderMehtod(orderMethod: string) {
   // alert("Here" + orderMethod);
  }
}
