import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LicenseAllListComponent } from './license-all-list.component';

describe('LicenseAllListComponent', () => {
  let component: LicenseAllListComponent;
  let fixture: ComponentFixture<LicenseAllListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LicenseAllListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LicenseAllListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
