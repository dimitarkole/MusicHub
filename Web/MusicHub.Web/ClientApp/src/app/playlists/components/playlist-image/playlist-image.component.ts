import { Component, Input, OnInit } from '@angular/core';
import { globalConstants } from '../../../common/global-constants';

@Component({
  selector: 'app-playlist-image',
  templateUrl: './playlist-image.component.html',
  styleUrls: ['./playlist-image.component.css']
})
export class PlaylistImageComponent implements OnInit {
  @Input() playlistImage: string = "";
  @Input() size: string = "";
  defaulePlaylistImagePath: string = globalConstants.playlist.imageFolderPathToUpload + globalConstants.playlist.defauldImage;
  constructor() { }

  ngOnInit() {
  }
}
