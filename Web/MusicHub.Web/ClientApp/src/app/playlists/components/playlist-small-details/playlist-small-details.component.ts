import { Component, Input, OnInit } from '@angular/core';
import { globalConstants } from '../../../common/global-constants';
import Playlist from '../../models/playlist';

@Component({
  selector: 'app-playlist-small-details',
  templateUrl: './playlist-small-details.component.html',
  styleUrls: ['./playlist-small-details.component.css']
})
export class PlaylistSmallDetailsComponent implements OnInit {
  @Input() playlist: Playlist;
  @Input() type: string;
  defaulePlaylistImagePath: string = globalConstants.playlist.imageFolderPathToUpload + globalConstants.playlist.defauldImage;
  constructor() { }

  ngOnInit() {
  }

}
