import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NoSearchResultsComponent } from './no-search-results.component';

describe('NoSearchResultsComponent', () => {
  let component: NoSearchResultsComponent;
  let fixture: ComponentFixture<NoSearchResultsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NoSearchResultsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NoSearchResultsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
