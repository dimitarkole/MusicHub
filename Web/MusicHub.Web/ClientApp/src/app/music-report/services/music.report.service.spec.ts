import { TestBed } from '@angular/core/testing';
import { MusicReportService } from './music.report.service';


describe('MusicReportService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: MusicReportService = TestBed.get(MusicReportService);
    expect(service).toBeTruthy();
  });
});
