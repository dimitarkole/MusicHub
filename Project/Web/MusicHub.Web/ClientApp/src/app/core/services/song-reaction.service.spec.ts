import { TestBed } from '@angular/core/testing';

import { SongReactionService } from './song-reaction.service';

describe('SongReactionService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: SongReactionService = TestBed.get(SongReactionService);
    expect(service).toBeTruthy();
  });
});
