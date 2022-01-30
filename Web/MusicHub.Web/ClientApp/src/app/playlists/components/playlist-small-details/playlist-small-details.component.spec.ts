import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PlaylistSmallDetailsComponent } from './playlist-small-details.component';

describe('PlaylistSmallDetailsComponent', () => {
  let component: PlaylistSmallDetailsComponent;
  let fixture: ComponentFixture<PlaylistSmallDetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PlaylistSmallDetailsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PlaylistSmallDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
