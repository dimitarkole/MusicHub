import { TestBed } from '@angular/core/testing';

import { RoleAnalysisService } from './role-analysis.service';

describe('RoleAnalysisService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: RoleAnalysisService = TestBed.get(RoleAnalysisService);
    expect(service).toBeTruthy();
  });
});
