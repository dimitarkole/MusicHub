import { TestBed } from '@angular/core/testing';

import { LisenedSongService } from './lisened-song.service';

describe('LisenedSongService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: LisenedSongService = TestBed.get(LisenedSongService);
    expect(service).toBeTruthy();
  });
});
