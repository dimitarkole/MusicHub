import { Component, HostListener, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { globalConstants } from '../../../common/global-constants';
import { BooleanSearchService } from '../../../core/resolvers/boolean-search.service';
import { BooleanSearchEntity } from '../../../shared/models/boolean-search';
import { AlbumPagination } from '../../models/albumPagination';
import { AlbumService } from '../../services/album.service';

@Component({
  selector: 'app-albums-list',
  templateUrl: './albums-list.component.html',
  styleUrls: ['./albums-list.component.css']
})
export class AlbumsListComponent implements OnInit {
  albumPagination: AlbumPagination = { currentPage: 0, numberOfPages: 1, albums: [] };
  search: string = "";
  isSearching: boolean = false;

  constructor(private albumService: AlbumService,
    private searchService: BooleanSearchService,
    private route: ActivatedRoute,
    private formBuilder: FormBuilder) {
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
      this.feachData();
    }
  }

  private feachData(): void {
    if (this.albumPagination.currentPage < this.albumPagination.numberOfPages) {
      this.route.queryParams.subscribe(queryParams => {
        let query = queryParams.searchQuery;
        if (!query) {
          if (this.isSearching == true) {
            this.ResetData(query);
            this.isSearching = false;
          }

          var page = this.albumPagination.currentPage + 1;
          this.albumService.get(page).subscribe(data => {
            this.albumPagination.numberOfPages = data.numberOfPages;
            data.albums.forEach(music => {
              this.albumPagination.albums.push(music);
            });
          });
        }
        else {
          this.isSearching = true;
          if (this.search != query) {
            this.ResetData(query);
          }

          var page = this.albumPagination.currentPage + 1;
          this.searchService.search<AlbumPagination>({ query }, BooleanSearchEntity.Albums, page).subscribe(data => {
            this.albumPagination.numberOfPages = data.numberOfPages
            this.albumPagination.numberOfPages = data.numberOfPages;
            data.albums.forEach(music => {
              this.albumPagination.albums.push(music);
            });
          });
        }

        this.albumPagination.currentPage = page;
      });
    }
  }

  private ResetData(query: any) {
    this.search = query;
    this.albumPagination = { currentPage: 0, numberOfPages: 1, albums: [] };
  }
}
