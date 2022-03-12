import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LikedSongsListComponent } from './liked-songs-list.component';

describe('LikedSongsListComponent', () => {
  let component: LikedSongsListComponent;
  let fixture: ComponentFixture<LikedSongsListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LikedSongsListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LikedSongsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
