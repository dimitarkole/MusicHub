import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RolesDetailsComponent } from './roles-details.component';

describe('RolesDetailsComponent', () => {
  let component: RolesDetailsComponent;
  let fixture: ComponentFixture<RolesDetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RolesDetailsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RolesDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
