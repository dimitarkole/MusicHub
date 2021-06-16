import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LicenseDetailsComponent } from './license-details.component';

describe('LicenseDetailsComponent', () => {
  let component: LicenseDetailsComponent;
  let fixture: ComponentFixture<LicenseDetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LicenseDetailsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LicenseDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
