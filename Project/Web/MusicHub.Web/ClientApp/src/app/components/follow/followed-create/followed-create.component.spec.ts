import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FollowedCreateComponent } from './followed-create.component';

describe('FollowedCreateComponent', () => {
  let component: FollowedCreateComponent;
  let fixture: ComponentFixture<FollowedCreateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FollowedCreateComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FollowedCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
