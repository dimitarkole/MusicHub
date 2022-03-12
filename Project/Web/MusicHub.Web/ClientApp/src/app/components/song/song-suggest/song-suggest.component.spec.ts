import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SongSuggestComponent } from './song-suggest.component';

describe('SongSuggestComponent', () => {
  let component: SongSuggestComponent;
  let fixture: ComponentFixture<SongSuggestComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SongSuggestComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SongSuggestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
