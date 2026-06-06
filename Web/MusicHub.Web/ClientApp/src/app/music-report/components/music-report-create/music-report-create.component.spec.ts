import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MusicReportCreateComponent } from './music-report-create.component';

describe('MusicReportCreateComponent', () => {
  let component: MusicReportCreateComponent;
  let fixture: ComponentFixture<MusicReportCreateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MusicReportCreateComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MusicReportCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
