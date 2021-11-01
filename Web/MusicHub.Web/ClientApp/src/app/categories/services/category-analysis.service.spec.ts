import { TestBed } from '@angular/core/testing';

import { CategoryAnalysisService } from './category-analysis.service';

describe('CategoryAnalysisService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: CategoryAnalysisService = TestBed.get(CategoryAnalysisService);
    expect(service).toBeTruthy();
  });
});
