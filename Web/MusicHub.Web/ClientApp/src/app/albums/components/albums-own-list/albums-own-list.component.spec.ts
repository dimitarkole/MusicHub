import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AlbumsOwnListComponent } from './albums-own-list.component';

describe('AlbumsOwnListComponent', () => {
  let component: AlbumsOwnListComponent;
  let fixture: ComponentFixture<AlbumsOwnListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AlbumsOwnListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AlbumsOwnListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
