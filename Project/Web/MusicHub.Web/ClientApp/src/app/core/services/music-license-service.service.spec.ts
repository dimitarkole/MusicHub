import { TestBed } from '@angular/core/testing';

import { MusicLicenseServiceService } from './music-license-service.service';

describe('MusicLicenseServiceService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: MusicLicenseServiceService = TestBed.get(MusicLicenseServiceService);
    expect(service).toBeTruthy();
  });
});
