import { Component, OnInit, Input, SimpleChanges, OnChanges } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { SongService } from '../../../core/services/song.service';
import { Song } from '../../shared/models/song';

@Component({
  selector: 'app-song-suggest',
  templateUrl: './song-suggest.component.html',
  styleUrls: ['./song-suggest.component.css']
})
export class SongSuggestComponent implements OnInit, OnChanges {
  @Input() songId: string;
  suggestSongs: Song[]

  constructor(
    private route: ActivatedRoute,
    private songService: SongService) {

  }

  ngOnChanges(changes: SimpleChanges): void {
    this.preparedPage();
  }

  ngOnInit() {
    this.preparedPage();
  }

  preparedPage() {
    this.songService.suggestSongs(this.songId).subscribe(data => {
      this.suggestSongs = data;
    });
  }
}
