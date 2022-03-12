import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PlaylistListAllComponent } from './playlist-list-all.component';

describe('PlaylistListAllComponent', () => {
  let component: PlaylistListAllComponent;
  let fixture: ComponentFixture<PlaylistListAllComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PlaylistListAllComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PlaylistListAllComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
