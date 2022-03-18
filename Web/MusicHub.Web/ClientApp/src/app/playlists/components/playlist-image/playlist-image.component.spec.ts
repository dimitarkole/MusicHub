import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PlaylistImageComponent } from './playlist-image.component';

describe('PlaylistImageComponent', () => {
  let component: PlaylistImageComponent;
  let fixture: ComponentFixture<PlaylistImageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PlaylistImageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PlaylistImageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
