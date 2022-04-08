import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NotificationSettingsListComponent } from './notification-settings-list.component';

describe('NotificationSettingsListComponent', () => {
  let component: NotificationSettingsListComponent;
  let fixture: ComponentFixture<NotificationSettingsListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NotificationSettingsListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NotificationSettingsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
