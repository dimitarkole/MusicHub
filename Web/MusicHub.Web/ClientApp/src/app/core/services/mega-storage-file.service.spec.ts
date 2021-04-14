import { TestBed } from '@angular/core/testing';

import { MegaStorageFileService } from './mega-storage-file.service';

describe('MegaStorageFileService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: MegaStorageFileService = TestBed.get(MegaStorageFileService);
    expect(service).toBeTruthy();
  });
});
