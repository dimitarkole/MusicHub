import { Component, Input, OnInit } from '@angular/core';
import { globalConstants } from '../../../common/global-constants';

@Component({
  selector: 'app-music-image',
  templateUrl: './music-image.component.html',
  styleUrls: ['./music-image.component.css']
})
export class MusicImageComponent implements OnInit {
  @Input() musicImage: string = "";
  @Input() size: string = "";
  musicFolderPath: string = globalConstants.music.imageFolderPathToUpload;
  defauleMusicImagePath: string = this.musicFolderPath + globalConstants.music.defauldImage;
  constructor() { }

  ngOnInit() {
  }
}
