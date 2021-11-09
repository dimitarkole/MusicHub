import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MusicHubBarChartComponent } from './music-hub-bar-chart.component';

describe('MusicHubBarChartComponent', () => {
  let component: MusicHubBarChartComponent;
  let fixture: ComponentFixture<MusicHubBarChartComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MusicHubBarChartComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MusicHubBarChartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
