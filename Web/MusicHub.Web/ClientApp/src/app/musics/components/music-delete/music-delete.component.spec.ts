import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MusicDeleteComponent } from './music-delete.component';

describe('MusicDeleteComponent', () => {
  let component: MusicDeleteComponent;
  let fixture: ComponentFixture<MusicDeleteComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MusicDeleteComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MusicDeleteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
