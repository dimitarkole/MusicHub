import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SongCreateComponent } from './song-create.component';

describe('SongCreateComponent', () => {
  let component: SongCreateComponent;
  let fixture: ComponentFixture<SongCreateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SongCreateComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SongCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
