import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ListenedSongHistoryDeleteComponent } from './listened-song-history-delete.component';

describe('ListenedSongHistoryDeleteComponent', () => {
  let component: ListenedSongHistoryDeleteComponent;
  let fixture: ComponentFixture<ListenedSongHistoryDeleteComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ListenedSongHistoryDeleteComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ListenedSongHistoryDeleteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
