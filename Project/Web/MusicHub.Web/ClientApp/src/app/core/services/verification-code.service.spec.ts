import { TestBed } from '@angular/core/testing';

import { VerificationCodeService } from './verification-code.service';

describe('VerificationCodeService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: VerificationCodeService = TestBed.get(VerificationCodeService);
    expect(service).toBeTruthy();
  });
});
