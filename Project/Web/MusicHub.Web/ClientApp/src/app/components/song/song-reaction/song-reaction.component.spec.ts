import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SongReactionComponent } from './song-reaction.component';

describe('SongReactionComponent', () => {
  let component: SongReactionComponent;
  let fixture: ComponentFixture<SongReactionComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SongReactionComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SongReactionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
