import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PlayLaterMusicPlayComponent } from './play-later-music-play.component';

describe('PlayLaterMusicPlayComponent', () => {
  let component: PlayLaterMusicPlayComponent;
  let fixture: ComponentFixture<PlayLaterMusicPlayComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PlayLaterMusicPlayComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PlayLaterMusicPlayComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
