import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material';
import { Router } from '@angular/router';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { ModalComponent } from 'ngb-modal';
import { AuthService } from '../../../core/services/auth.service';
import Category from '../../models/category';
import { CategoryService } from '../../services/category.service';

@Component({
  selector: 'app-category-create',
  templateUrl: './category-create.component.html',
  styleUrls: ['./category-create.component.css'],
})
export class CategoryCreateComponent implements OnInit {
  category: Category;
  nameMinLength = 2;
  nameMaxLength = 30;
  categoryForm: FormGroup

  constructor(public dialogRef: MatDialogRef<ModalComponent>,
    private categoryService: CategoryService,
    private formBuilder: FormBuilder,
  ) { }

  ngOnInit() {
   this.categoryForm = this.formBuilder.group({
      name: [
        null,
        [
          Validators.required,
          Validators.minLength(this.nameMinLength),
          Validators.maxLength(this.nameMaxLength)
        ]
      ],
    })
  }

  // If the user clicks the cancel button a.k.a. the go back button, then\
  // just close the modal
  closeModal() {
    this.dialogRef.close();
  }

  OnSubmit() {
   let category: Category = this.categoryForm.value;
    this.categoryService.create(category)
      .subscribe(_ => {
        this.dialogRef.close(); //It closes successfully
      })
  }

  get name(): AbstractControl {
    return this.categoryForm.get('name');
  }
}
