import { TestBed } from '@angular/core/testing';

import { NotificationSettingsService } from './notification-settings.service';

describe('NotificationSettingsService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: NotificationSettingsService = TestBed.get(NotificationSettingsService);
    expect(service).toBeTruthy();
  });
});
