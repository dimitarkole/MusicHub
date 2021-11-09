import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RoleAnalysisComponent } from './role-analysis.component';

describe('RoleAnalysisComponent', () => {
  let component: RoleAnalysisComponent;
  let fixture: ComponentFixture<RoleAnalysisComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RoleAnalysisComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RoleAnalysisComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
