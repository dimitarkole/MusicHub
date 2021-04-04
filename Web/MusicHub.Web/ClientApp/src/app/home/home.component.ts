import { Component, HostListener, Input, OnInit } from '@angular/core';
import { globalConstants } from '../common/global-constants';
import { NgbActiveModal, NgbModal, NgbModalConfig } from '@ng-bootstrap/ng-bootstrap';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators, AbstractControl } from '@angular/forms';
import { Observable } from 'rxjs';
import { CategoryCreateComponent } from '../categories/components/category-create/category-create.component';
import { CategoryDeleteComponent } from '../categories/components/category-delete/category-delete.component';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
    // add NgbModalConfig and NgbModal to the component providers
})

export class HomeComponent {

  constructor() { }


}
