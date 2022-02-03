import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NotificationSpecialInfoComponent } from './notification-special-info.component';

describe('NotificationSpecialInfoComponent', () => {
  let component: NotificationSpecialInfoComponent;
  let fixture: ComponentFixture<NotificationSpecialInfoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NotificationSpecialInfoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NotificationSpecialInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
