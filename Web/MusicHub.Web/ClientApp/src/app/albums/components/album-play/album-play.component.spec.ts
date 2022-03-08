import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AlbumPlayComponent } from './album-play.component';

describe('AlbumPlayComponent', () => {
  let component: AlbumPlayComponent;
  let fixture: ComponentFixture<AlbumPlayComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AlbumPlayComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AlbumPlayComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
