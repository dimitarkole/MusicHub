import { TestBed } from '@angular/core/testing';

import { PlayLaterMusicService } from './play-later-music.service';

describe('PlayLaterMusicService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: PlayLaterMusicService = TestBed.get(PlayLaterMusicService);
    expect(service).toBeTruthy();
  });
});
