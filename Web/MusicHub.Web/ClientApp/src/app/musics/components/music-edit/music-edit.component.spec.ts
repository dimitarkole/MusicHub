import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MusicEditComponent } from './music-edit.component';

describe('MusicEditComponent', () => {
  let component: MusicEditComponent;
  let fixture: ComponentFixture<MusicEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MusicEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MusicEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
