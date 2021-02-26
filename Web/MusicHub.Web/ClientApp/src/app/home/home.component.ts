import { Component, HostListener, Input, OnInit } from '@angular/core';
import { globalConstants } from '../common/global-constants';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators, AbstractControl } from '@angular/forms';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  constructor(private modalService: NgbModal,
    private router: Router,
    private formBuilder: FormBuilder) {
  }

  ngOnInit(): void {
  }
}
