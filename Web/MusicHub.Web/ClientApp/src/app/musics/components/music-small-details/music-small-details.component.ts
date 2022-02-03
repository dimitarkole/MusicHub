import { Input } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { globalConstants } from '../../../common/global-constants';
import { Music } from '../../models/music';

@Component({
  selector: 'app-music-small-details',
  templateUrl: './music-small-details.component.html',
  styleUrls: ['./music-small-details.component.css']
})
export class MusicSmallDetailsComponent implements OnInit {
  @Input() music: Music;
  @Input() type: string;
  musicAudioPath: string = globalConstants.music.audioFolderPath;
  musicFolderPath: string = globalConstants.music.imageFolderPathToUpload;
  defauleMusicImagePath: string = this.musicFolderPath + globalConstants.music.defauldImage;
  constructor() { }

  ngOnInit() {
  }

}
