import { TestBed } from '@angular/core/testing';

import { PlaylistMusicsService } from './playlist-musics.service';

describe('PlaylistMusicsService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: PlaylistMusicsService = TestBed.get(PlaylistMusicsService);
    expect(service).toBeTruthy();
  });
});
