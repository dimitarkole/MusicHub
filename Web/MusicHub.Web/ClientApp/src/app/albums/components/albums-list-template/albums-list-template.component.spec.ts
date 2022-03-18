import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AlbumsListTemplateComponent } from './albums-list-template.component';

describe('AlbumsListTemplateComponent', () => {
  let component: AlbumsListTemplateComponent;
  let fixture: ComponentFixture<AlbumsListTemplateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AlbumsListTemplateComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AlbumsListTemplateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
