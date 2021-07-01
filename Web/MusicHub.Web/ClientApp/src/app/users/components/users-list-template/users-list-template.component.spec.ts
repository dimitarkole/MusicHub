import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UsersListTemplateComponent } from './users-list-template.component';

describe('UsersListTemplateComponent', () => {
  let component: UsersListTemplateComponent;
  let fixture: ComponentFixture<UsersListTemplateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UsersListTemplateComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UsersListTemplateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
