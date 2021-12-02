import { TestBed } from '@angular/core/testing';

import { PlayedMusicService } from './played-music.service';

describe('PlayedMusicService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: PlayedMusicService = TestBed.get(PlayedMusicService);
    expect(service).toBeTruthy();
  });
});
