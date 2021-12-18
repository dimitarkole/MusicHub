import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { OrderByDropDownComponent } from './order-by-drop-down.component';

describe('OrderByDropDownComponent', () => {
  let component: OrderByDropDownComponent;
  let fixture: ComponentFixture<OrderByDropDownComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OrderByDropDownComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OrderByDropDownComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
