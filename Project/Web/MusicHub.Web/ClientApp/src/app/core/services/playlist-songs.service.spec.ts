import { TestBed } from '@angular/core/testing';

import { PlaylistSongsService } from './playlist-songs.service';

describe('PlaylistSongsService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: PlaylistSongsService = TestBed.get(PlaylistSongsService);
    expect(service).toBeTruthy();
  });
});
