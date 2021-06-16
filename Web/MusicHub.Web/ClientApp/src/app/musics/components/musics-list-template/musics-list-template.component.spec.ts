import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MusicsListTemplateComponent } from './musics-list-template.component';

describe('MusicsListTemplateComponent', () => {
  let component: MusicsListTemplateComponent;
  let fixture: ComponentFixture<MusicsListTemplateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MusicsListTemplateComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MusicsListTemplateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
