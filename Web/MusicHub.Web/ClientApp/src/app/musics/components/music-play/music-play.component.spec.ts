import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MusicPlayComponent } from './music-play.component';

describe('MusicPlayComponent', () => {
  let component: MusicPlayComponent;
  let fixture: ComponentFixture<MusicPlayComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MusicPlayComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MusicPlayComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
