import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PlayLaterMusicDeleteComponent } from './play-later-music-delete.component';

describe('PlayLaterMusicDeleteComponent', () => {
  let component: PlayLaterMusicDeleteComponent;
  let fixture: ComponentFixture<PlayLaterMusicDeleteComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PlayLaterMusicDeleteComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PlayLaterMusicDeleteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
