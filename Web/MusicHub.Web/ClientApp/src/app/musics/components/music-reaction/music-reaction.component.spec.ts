import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MusicReactionComponent } from './music-reaction.component';

describe('MusicReactionComponent', () => {
  let component: MusicReactionComponent;
  let fixture: ComponentFixture<MusicReactionComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MusicReactionComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MusicReactionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
