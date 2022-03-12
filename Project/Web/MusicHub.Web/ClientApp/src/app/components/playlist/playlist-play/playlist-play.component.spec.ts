import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PlaylistPlayComponent } from './playlist-play.component';

describe('PlaylistPlayComponent', () => {
  let component: PlaylistPlayComponent;
  let fixture: ComponentFixture<PlaylistPlayComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PlaylistPlayComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PlaylistPlayComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
