import { Component, Input, OnInit } from '@angular/core';
import { PlayedMusic } from '../../models/playedMusic';

@Component({
  selector: 'app-played-music-details',
  templateUrl: './played-music-details.component.html',
  styleUrls: ['./played-music-details.component.css']
})
export class PlayedMusicDetailsComponent implements OnInit {
  @Input() playedMusic: PlayedMusic;
  constructor() { }

  ngOnInit() {
  }

}
