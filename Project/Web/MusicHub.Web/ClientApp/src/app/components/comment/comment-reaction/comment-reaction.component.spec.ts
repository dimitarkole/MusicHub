import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CommentReactionComponent } from './comment-reaction.component';

describe('CommentReactionComponent', () => {
  let component: CommentReactionComponent;
  let fixture: ComponentFixture<CommentReactionComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CommentReactionComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CommentReactionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
