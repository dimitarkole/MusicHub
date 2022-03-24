import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LikedMusicDetailsComponent } from './liked-music-details.component';

describe('LikedMusicDetailsComponent', () => {
  let component: LikedMusicDetailsComponent;
  let fixture: ComponentFixture<LikedMusicDetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LikedMusicDetailsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LikedMusicDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
