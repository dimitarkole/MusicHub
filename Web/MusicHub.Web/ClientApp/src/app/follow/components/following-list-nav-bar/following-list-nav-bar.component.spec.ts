import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FollowingListNavBarComponent } from './following-list-nav-bar.component';

describe('FollowingListNavBarComponent', () => {
  let component: FollowingListNavBarComponent;
  let fixture: ComponentFixture<FollowingListNavBarComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FollowingListNavBarComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FollowingListNavBarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
