import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UserIpRuleCreateComponent } from './user-ip-rule-create.component';

describe('UserIpRuleCreateComponent', () => {
  let component: UserIpRuleCreateComponent;
  let fixture: ComponentFixture<UserIpRuleCreateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UserIpRuleCreateComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UserIpRuleCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
