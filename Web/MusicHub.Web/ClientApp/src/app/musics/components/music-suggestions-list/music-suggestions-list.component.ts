import { Component, HostListener, Input, OnInit, SimpleChanges } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { globalConstants } from '../../../common/global-constants';
import { MusicPagination } from '../../models/musicPagination';
import { MusicService } from '../../services/music.service';

@Component({
  selector: 'app-music-suggestions-list',
  templateUrl: './music-suggestions-list.component.html',
  styleUrls: ['./music-suggestions-list.component.css']
})
export class MusicSuggestionsListComponent implements OnInit {
  @Input() musicId: string;
  suggestMusicPagination: MusicPagination = { currentPage: 0, numberOfPages: 1, musics: [] };
  musicAudioPath: string = globalConstants.music.audioFolderPath;
  musicFolderPath: string = globalConstants.music.imageFolderPathToUpload;
  defauleMusicImagePath: string = this.musicFolderPath + globalConstants.music.defauldImage;

  constructor(
    private route: ActivatedRoute,
    private musicService: MusicService) {

  }

  ngOnChanges(changes: SimpleChanges): void {
    this.getSuggetstMusics();
  }

  ngOnInit() {
    this.getSuggetstMusics();
  }

  @HostListener("window:scroll", [])
  onWindowScroll() {
    //In chrome and some browser scroll is given to body tag
    let pos = (document.documentElement.scrollTop || document.body.scrollTop) + document.documentElement.offsetHeight;
    let max = document.documentElement.scrollHeight;
    // pos/max will give you the distance between scroll bottom and and bottom of screen in percentage.
    if (pos > max * globalConstants.pagination.updateProcent) {
      //Do your action here
      this.getSuggetstMusics();
    }
  }

  getSuggetstMusics() {
    var page = this.suggestMusicPagination.currentPage + 1;
    if (page <= this.suggestMusicPagination.numberOfPages) {
      this.suggestMusicPagination.currentPage = page;
      this.musicService.suggestMusics(this.musicId, page).subscribe(data => {
        this.suggestMusicPagination.numberOfPages = data.numberOfPages;
        data.musics.forEach(song => {
          this.suggestMusicPagination.musics.push(song);
        });
      });
    }
  }
}
