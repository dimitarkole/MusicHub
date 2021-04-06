import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PlaylistsListTemplateComponent } from './playlists-list-template.component';

describe('PlaylistsListTemplateComponent', () => {
  let component: PlaylistsListTemplateComponent;
  let fixture: ComponentFixture<PlaylistsListTemplateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PlaylistsListTemplateComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PlaylistsListTemplateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
