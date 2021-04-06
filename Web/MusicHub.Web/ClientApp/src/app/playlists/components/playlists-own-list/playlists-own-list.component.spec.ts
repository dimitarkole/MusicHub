import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PlaylistsOwnListComponent } from './playlists-own-list.component';

describe('PlaylistsOwnListComponent', () => {
  let component: PlaylistsOwnListComponent;
  let fixture: ComponentFixture<PlaylistsOwnListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PlaylistsOwnListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PlaylistsOwnListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
