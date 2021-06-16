import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LicenseOwnListComponent } from './license-own-list.component';

describe('LicenseOwnListComponent', () => {
  let component: LicenseOwnListComponent;
  let fixture: ComponentFixture<LicenseOwnListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LicenseOwnListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LicenseOwnListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
