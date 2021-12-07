import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MusicImageComponent } from './music-image.component';

describe('MusicImageComponent', () => {
  let component: MusicImageComponent;
  let fixture: ComponentFixture<MusicImageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MusicImageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MusicImageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
