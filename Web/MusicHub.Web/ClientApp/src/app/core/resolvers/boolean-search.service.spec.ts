import { TestBed } from '@angular/core/testing';

import { BooleanSearchService } from './boolean-search.service';

describe('BooleanSearchService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: BooleanSearchService = TestBed.get(BooleanSearchService);
    expect(service).toBeTruthy();
  });
});
