import { TestBed } from '@angular/core/testing';

import { LicenseService } from './license.service';

describe('LicenseService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: LicenseService = TestBed.get(LicenseService);
    expect(service).toBeTruthy();
  });
});
