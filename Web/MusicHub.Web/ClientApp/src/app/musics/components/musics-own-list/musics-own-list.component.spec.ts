import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MusicsOwnListComponent } from './musics-own-list.component';

describe('MusicsOwnListComponent', () => {
  let component: MusicsOwnListComponent;
  let fixture: ComponentFixture<MusicsOwnListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MusicsOwnListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MusicsOwnListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
