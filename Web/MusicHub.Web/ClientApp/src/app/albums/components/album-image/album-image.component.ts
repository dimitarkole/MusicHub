import { Component, Input, OnInit } from '@angular/core';
import { globalConstants } from '../../../common/global-constants';

@Component({
  selector: 'app-album-image',
  templateUrl: './album-image.component.html',
  styleUrls: ['./album-image.component.css']
})
export class AlbumImageComponent implements OnInit {
  @Input() albumImage: string = "";
  @Input() size: string = "";
  defaultAlbumImagePath: string = globalConstants.album.imageFolderPathToUpload + globalConstants.album.defauldImage;
  constructor() { }

  ngOnInit() {
  }
}
