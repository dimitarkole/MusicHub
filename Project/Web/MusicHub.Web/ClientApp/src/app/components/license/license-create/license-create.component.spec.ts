import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LicenseCreateComponent } from './license-create.component';

describe('LicenseCreateComponent', () => {
  let component: LicenseCreateComponent;
  let fixture: ComponentFixture<LicenseCreateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LicenseCreateComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LicenseCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
