import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PlaylistsListComponent } from './playlists-list.component';

describe('PlaylistsListComponent', () => {
  let component: PlaylistsListComponent;
  let fixture: ComponentFixture<PlaylistsListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PlaylistsListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PlaylistsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
