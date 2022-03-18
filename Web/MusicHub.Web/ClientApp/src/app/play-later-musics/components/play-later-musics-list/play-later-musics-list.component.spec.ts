import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PlayLaterMusicsListComponent } from './play-later-musics-list.component';

describe('PlayLaterMusicsListComponent', () => {
  let component: PlayLaterMusicsListComponent;
  let fixture: ComponentFixture<PlayLaterMusicsListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PlayLaterMusicsListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PlayLaterMusicsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
