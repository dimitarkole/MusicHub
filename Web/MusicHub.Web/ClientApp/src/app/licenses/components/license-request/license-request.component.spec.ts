import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LicenseRequestComponent } from './license-request.component';

describe('LicenseRequestComponent', () => {
  let component: LicenseRequestComponent;
  let fixture: ComponentFixture<LicenseRequestComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LicenseRequestComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LicenseRequestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
