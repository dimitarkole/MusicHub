import { TestBed } from '@angular/core/testing';

import { MusicReactionService } from './music-reaction.service';

describe('MusicReactionService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: MusicReactionService = TestBed.get(MusicReactionService);
    expect(service).toBeTruthy();
  });
});
