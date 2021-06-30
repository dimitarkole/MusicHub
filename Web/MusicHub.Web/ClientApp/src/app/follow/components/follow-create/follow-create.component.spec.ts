import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FollowCreateComponent } from './follow-create.component';

describe('FollowCreateComponent', () => {
  let component: FollowCreateComponent;
  let fixture: ComponentFixture<FollowCreateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FollowCreateComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FollowCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
