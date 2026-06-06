import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddMusicToAlbumComponent } from './add-music-to-album.component';

describe('AddMusicToAlbumComponent', () => {
  let component: AddMusicToAlbumComponent;
  let fixture: ComponentFixture<AddMusicToAlbumComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddMusicToAlbumComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddMusicToAlbumComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
