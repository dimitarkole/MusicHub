import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SongDeleteModalComponent } from './song-delete-modal.component';

describe('SongDeleteModalComponent', () => {
  let component: SongDeleteModalComponent;
  let fixture: ComponentFixture<SongDeleteModalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SongDeleteModalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SongDeleteModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
