import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AlbumSmallDetailsComponent } from './album-small-details.component';

describe('AlbumSmallDetailsComponent', () => {
  let component: AlbumSmallDetailsComponent;
  let fixture: ComponentFixture<AlbumSmallDetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AlbumSmallDetailsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AlbumSmallDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
