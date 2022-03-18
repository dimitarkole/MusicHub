import { Component, Input, OnInit } from '@angular/core';
import { globalConstants } from '../../../common/global-constants';
import Album from '../../models/album';

@Component({
  selector: 'app-album-small-details',
  templateUrl: './album-small-details.component.html',
  styleUrls: ['./album-small-details.component.css']
})
export class AlbumSmallDetailsComponent implements OnInit {
  @Input() album: Album;
  @Input() type: string;
  defauleAlbumImagePath: string = globalConstants.album.imageFolderPathToUpload + globalConstants.album.defauldImage;
  constructor() { }

  ngOnInit() {
  }
}

