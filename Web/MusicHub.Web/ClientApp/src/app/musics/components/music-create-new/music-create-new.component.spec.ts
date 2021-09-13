import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MusicCreateNewComponent } from './music-create-new.component';

describe('MusicCreateNewComponent', () => {
  let component: MusicCreateNewComponent;
  let fixture: ComponentFixture<MusicCreateNewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MusicCreateNewComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MusicCreateNewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
