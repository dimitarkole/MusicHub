import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddMusicToPlaylistsComponent } from './add-music-to-playlists.component';

describe('AddMusicToPlaylistsComponent', () => {
  let component: AddMusicToPlaylistsComponent;
  let fixture: ComponentFixture<AddMusicToPlaylistsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddMusicToPlaylistsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddMusicToPlaylistsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
