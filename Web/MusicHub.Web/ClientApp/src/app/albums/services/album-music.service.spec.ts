import { TestBed } from '@angular/core/testing';

import { AlbumMusicService } from './album-music.service';

describe('AlbumMusicService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: AlbumMusicService = TestBed.get(AlbumMusicService);
    expect(service).toBeTruthy();
  });
});
