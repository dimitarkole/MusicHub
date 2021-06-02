import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProfileOwnComponent } from './profile-own.component';

describe('ProfileOwnComponent', () => {
  let component: ProfileOwnComponent;
  let fixture: ComponentFixture<ProfileOwnComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProfileOwnComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProfileOwnComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
