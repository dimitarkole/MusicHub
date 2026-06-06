import { TestBed } from '@angular/core/testing';

import { UserIpRuleService } from './user-ip-rule.service';

describe('UserIpRuleService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: UserIpRuleService = TestBed.get(UserIpRuleService);
    expect(service).toBeTruthy();
  });
});
