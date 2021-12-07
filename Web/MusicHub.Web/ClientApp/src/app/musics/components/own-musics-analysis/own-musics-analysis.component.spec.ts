import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { OwnMusicsAnalysisComponent } from './own-musics-analysis.component';

describe('OwnMusicsAnalysisComponent', () => {
  let component: OwnMusicsAnalysisComponent;
  let fixture: ComponentFixture<OwnMusicsAnalysisComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OwnMusicsAnalysisComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OwnMusicsAnalysisComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
