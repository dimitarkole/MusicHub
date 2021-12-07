import { TestBed } from '@angular/core/testing';

import { OwnMusicsAnalysisService } from './own-musics-analysis.service';

describe('OwnMusicsAnalysisService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: OwnMusicsAnalysisService = TestBed.get(OwnMusicsAnalysisService);
    expect(service).toBeTruthy();
  });
});
