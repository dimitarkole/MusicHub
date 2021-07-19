import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NoMoreResultComponent } from './no-more-result.component';

describe('NoMoreResultComponent', () => {
  let component: NoMoreResultComponent;
  let fixture: ComponentFixture<NoMoreResultComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NoMoreResultComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NoMoreResultComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
