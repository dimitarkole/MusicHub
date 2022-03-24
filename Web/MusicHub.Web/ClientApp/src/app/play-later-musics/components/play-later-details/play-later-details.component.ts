import { Component, Input, OnInit } from '@angular/core';
import { PlayLaterMusic } from '../../models/playLaterMusic';

@Component({
  selector: 'app-play-later-details',
  templateUrl: './play-later-details.component.html',
  styleUrls: ['./play-later-details.component.css']
})
export class PlayLaterDetailsComponent implements OnInit {
  @Input() playLaterMusic: PlayLaterMusic;
  constructor() { }

  ngOnInit() {
  }

}
