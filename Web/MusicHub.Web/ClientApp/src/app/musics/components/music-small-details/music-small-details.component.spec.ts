import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MusicSmallDetailsComponent } from './music-small-details.component';

describe('MusicSmallDetailsComponent', () => {
  let component: MusicSmallDetailsComponent;
  let fixture: ComponentFixture<MusicSmallDetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MusicSmallDetailsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MusicSmallDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
