import { Component, Input, OnInit } from '@angular/core';
import { ReactionInfo } from '../../../shared/models/reactionInfo';
import { LikedMusicPagination } from '../../models/likedMusicPagination';

@Component({
  selector: 'app-liked-music-details',
  templateUrl: './liked-music-details.component.html',
  styleUrls: ['./liked-music-details.component.css']
})
export class LikedMusicDetailsComponent implements OnInit {
  @Input() likedMusic: ReactionInfo;
  constructor() { }

  ngOnInit() {
  }

}
