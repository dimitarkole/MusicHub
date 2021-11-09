import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CategoryAnalysisComponent } from './category-analysis.component';

describe('CategoryAnalysisComponent', () => {
  let component: CategoryAnalysisComponent;
  let fixture: ComponentFixture<CategoryAnalysisComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CategoryAnalysisComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CategoryAnalysisComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
