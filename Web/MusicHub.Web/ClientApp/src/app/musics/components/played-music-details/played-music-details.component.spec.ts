import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PlayedMusicDetailsComponent } from './played-music-details.component';

describe('PlayedMusicDetailsComponent', () => {
  let component: PlayedMusicDetailsComponent;
  let fixture: ComponentFixture<PlayedMusicDetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PlayedMusicDetailsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PlayedMusicDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
