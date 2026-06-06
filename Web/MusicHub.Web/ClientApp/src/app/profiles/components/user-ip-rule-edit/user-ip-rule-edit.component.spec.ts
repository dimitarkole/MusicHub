import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UserIpRuleEditComponent } from './user-ip-rule-edit.component';

describe('UserIpRuleEditComponent', () => {
  let component: UserIpRuleEditComponent;
  let fixture: ComponentFixture<UserIpRuleEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UserIpRuleEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UserIpRuleEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
