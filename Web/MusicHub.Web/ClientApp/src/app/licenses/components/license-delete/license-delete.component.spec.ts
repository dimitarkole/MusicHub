import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LicenseDeleteComponent } from './license-delete.component';

describe('LicenseDeleteComponent', () => {
  let component: LicenseDeleteComponent;
  let fixture: ComponentFixture<LicenseDeleteComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LicenseDeleteComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LicenseDeleteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
