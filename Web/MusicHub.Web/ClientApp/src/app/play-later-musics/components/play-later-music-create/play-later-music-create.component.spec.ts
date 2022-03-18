import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PlayLaterMusicCreateComponent } from './play-later-music-create.component';

describe('PlayLaterMusicCreateComponent', () => {
  let component: PlayLaterMusicCreateComponent;
  let fixture: ComponentFixture<PlayLaterMusicCreateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PlayLaterMusicCreateComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PlayLaterMusicCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
