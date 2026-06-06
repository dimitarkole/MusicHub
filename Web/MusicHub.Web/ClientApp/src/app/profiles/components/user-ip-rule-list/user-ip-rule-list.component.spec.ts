import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UserIpRuleListComponent } from './user-ip-rule-list.component';

describe('UserIpRuleListComponent', () => {
  let component: UserIpRuleListComponent;
  let fixture: ComponentFixture<UserIpRuleListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UserIpRuleListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UserIpRuleListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
