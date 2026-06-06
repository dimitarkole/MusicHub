import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddMusicToAlbumsComponent } from './add-music-to-albums.component';

describe('AddMusicToAlbumsComponent', () => {
  let component: AddMusicToAlbumsComponent;
  let fixture: ComponentFixture<AddMusicToAlbumsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddMusicToAlbumsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddMusicToAlbumsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
