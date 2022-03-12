import { TestBed } from '@angular/core/testing';

import { CommentReactionService } from './comment-reaction.service';

describe('CommentReactionService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: CommentReactionService = TestBed.get(CommentReactionService);
    expect(service).toBeTruthy();
  });
});
