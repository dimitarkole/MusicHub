import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PlayLaterDetailsComponent } from './play-later-details.component';

describe('PlayLaterDetailsComponent', () => {
  let component: PlayLaterDetailsComponent;
  let fixture: ComponentFixture<PlayLaterDetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PlayLaterDetailsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PlayLaterDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
