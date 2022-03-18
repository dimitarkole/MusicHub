import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-music-add-to',
  templateUrl: './music-add-to.component.html',
  styleUrls: ['./music-add-to.component.css']
})
export class MusicAddToComponent implements OnInit {
  @Input() musicId: string;
  constructor() { }

  ngOnInit() {
  }

}
