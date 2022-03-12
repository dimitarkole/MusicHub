import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PlaylistListTemplateComponent } from './playlist-list-template.component';

describe('PlaylistListTemplateComponent', () => {
  let component: PlaylistListTemplateComponent;
  let fixture: ComponentFixture<PlaylistListTemplateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PlaylistListTemplateComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PlaylistListTemplateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
