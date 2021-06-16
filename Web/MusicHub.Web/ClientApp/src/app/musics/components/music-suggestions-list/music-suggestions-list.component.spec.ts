import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MusicSuggestionsListComponent } from './music-suggestions-list.component';

describe('MusicSuggestionsListComponent', () => {
  let component: MusicSuggestionsListComponent;
  let fixture: ComponentFixture<MusicSuggestionsListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MusicSuggestionsListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MusicSuggestionsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
