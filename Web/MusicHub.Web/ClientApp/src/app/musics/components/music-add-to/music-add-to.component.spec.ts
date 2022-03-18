import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MusicAddToComponent } from './music-add-to.component';

describe('MusicAddToComponent', () => {
  let component: MusicAddToComponent;
  let fixture: ComponentFixture<MusicAddToComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MusicAddToComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MusicAddToComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
