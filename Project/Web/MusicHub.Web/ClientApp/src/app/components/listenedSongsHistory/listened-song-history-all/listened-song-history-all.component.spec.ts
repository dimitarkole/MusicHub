import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ListenedSongHistoryAllComponent } from './listened-song-history-all.component';

describe('ListenedSongHistoryAllComponent', () => {
  let component: ListenedSongHistoryAllComponent;
  let fixture: ComponentFixture<ListenedSongHistoryAllComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ListenedSongHistoryAllComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ListenedSongHistoryAllComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
