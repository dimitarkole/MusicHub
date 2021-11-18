import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LikedMusicsListComponent } from './liked-musics-list.component';

describe('LikedMusicsListComponent', () => {
  let component: LikedMusicsListComponent;
  let fixture: ComponentFixture<LikedMusicsListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LikedMusicsListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LikedMusicsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
