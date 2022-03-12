import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LicenseListTemplateComponent } from './license-list-template.component';

describe('LicenseListTemplateComponent', () => {
  let component: LicenseListTemplateComponent;
  let fixture: ComponentFixture<LicenseListTemplateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LicenseListTemplateComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LicenseListTemplateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
