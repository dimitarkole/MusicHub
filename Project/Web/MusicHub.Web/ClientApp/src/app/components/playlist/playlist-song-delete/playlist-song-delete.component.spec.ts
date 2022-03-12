import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PlaylistSongDeleteComponent } from './playlist-song-delete.component';

describe('PlaylistSongDeleteComponent', () => {
  let component: PlaylistSongDeleteComponent;
  let fixture: ComponentFixture<PlaylistSongDeleteComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PlaylistSongDeleteComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PlaylistSongDeleteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
