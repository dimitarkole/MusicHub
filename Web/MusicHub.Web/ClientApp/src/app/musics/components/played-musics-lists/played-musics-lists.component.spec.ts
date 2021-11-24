import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PlayedMusicsListsComponent } from './played-musics-lists.component';

describe('PlayedMusicsListsComponent', () => {
  let component: PlayedMusicsListsComponent;
  let fixture: ComponentFixture<PlayedMusicsListsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PlayedMusicsListsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PlayedMusicsListsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
