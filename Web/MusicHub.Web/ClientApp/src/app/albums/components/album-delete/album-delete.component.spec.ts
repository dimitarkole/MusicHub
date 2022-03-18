import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AlbumDeleteComponent } from './album-delete.component';

describe('AlbumDeleteComponent', () => {
  let component: AlbumDeleteComponent;
  let fixture: ComponentFixture<AlbumDeleteComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AlbumDeleteComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AlbumDeleteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
