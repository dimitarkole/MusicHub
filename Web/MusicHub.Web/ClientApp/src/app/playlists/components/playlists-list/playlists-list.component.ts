import { Component, HostListener, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { globalConstants } from '../../../common/global-constants';
import { BooleanSearchService } from '../../../core/resolvers/boolean-search.service';
import { BooleanSearchEntity } from '../../../shared/models/boolean-search';
import { CreateOrderMethod, OrderMethod } from '../../../shared/models/orderMethod';
import Playlist from '../../models/playlist';
import { PlaylistPagination } from '../../models/playlistPagination';
import { PlaylistService } from '../../services/playlist.service';

@Component({
  selector: 'app-playlists-list',
  templateUrl: './playlists-list.component.html',
  styleUrls: ['./playlists-list.component.css']
})
export class PlaylistsListComponent implements OnInit {
  playlistPagination: PlaylistPagination = { currentPage: 0, numberOfPages: 1, playlists: [] };
  search: string = "";
  isSearching: boolean = false;

  constructor(private playlistService: PlaylistService,
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
    if (this.playlistPagination.currentPage < this.playlistPagination.numberOfPages) {
      this.route.queryParams.subscribe(queryParams => {
        let query = queryParams.searchQuery;
        if (!query) {
          if (this.isSearching == true) {
            this.ResetData(query);
            this.isSearching = false;
          }

          var page = this.playlistPagination.currentPage + 1;
          this.playlistService.get(page).subscribe(data => {
            this.playlistPagination.numberOfPages = data.numberOfPages;
            data.playlists.forEach(music => {
              this.playlistPagination.playlists.push(music);
            });
          });
        }
        else {
          this.isSearching = true;
          if (this.search != query) {
            this.ResetData(query);
          }

          var page = this.playlistPagination.currentPage + 1;
          this.searchService.search<PlaylistPagination>({ query }, BooleanSearchEntity.Playlists, page).subscribe(data => {
            this.playlistPagination.numberOfPages = data.numberOfPages
            this.playlistPagination.numberOfPages = data.numberOfPages;
            data.playlists.forEach(music => {
              this.playlistPagination.playlists.push(music);
            });
          });
        }

        this.playlistPagination.currentPage = page;
      });
    }
  }

  private ResetData(query: any) {
    this.search = query;
    this.playlistPagination = { currentPage: 0, numberOfPages: 1, playlists: [] };
  }
}
