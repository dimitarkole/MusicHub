import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MusicHubLineChartComponent } from './music-hub-line-chart.component';

describe('MusicHubLineChartComponent', () => {
  let component: MusicHubLineChartComponent;
  let fixture: ComponentFixture<MusicHubLineChartComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MusicHubLineChartComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MusicHubLineChartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
