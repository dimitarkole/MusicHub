import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FollowInfoComponent } from './follow-info.component';

describe('FollowInfoComponent', () => {
  let component: FollowInfoComponent;
  let fixture: ComponentFixture<FollowInfoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FollowInfoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FollowInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
