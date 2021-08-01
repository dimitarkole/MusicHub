import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ResendEmailConfirmComponent } from './resend-email-confirm.component';

describe('ResendEmailConfirmComponent', () => {
  let component: ResendEmailConfirmComponent;
  let fixture: ComponentFixture<ResendEmailConfirmComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ResendEmailConfirmComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ResendEmailConfirmComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
