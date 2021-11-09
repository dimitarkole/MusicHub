import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MusicHubPieChartComponent } from './music-hub-pie-chart.component';

describe('MusicHubPieChartComponent', () => {
  let component: MusicHubPieChartComponent;
  let fixture: ComponentFixture<MusicHubPieChartComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MusicHubPieChartComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MusicHubPieChartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
