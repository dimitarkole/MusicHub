import { Component, Inject, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { Router } from '@angular/router';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { ModalComponent } from 'ngb-modal';
import { AuthService } from '../../../core/services/auth.service';
import Category from '../../models/category';
import { CategoryService } from '../../services/category.service';

@Component({
  selector: 'app-category-edit',
  templateUrl: './category-edit.component.html',
  styleUrls: ['./category-edit.component.css']
})
export class CategoryEditComponent implements OnInit {
  nameMinLength = 2;
  nameMaxLength = 30;
  categoryForm: FormGroup

  constructor(
    private formBuilder: FormBuilder,
    @Inject(MAT_DIALOG_DATA) public data: any,
    public dialogRef: MatDialogRef<ModalComponent>,
    private categoryService: CategoryService) {
  }

  ngOnInit() {
    this.categoryForm = this.formBuilder.group({
      name: [
        this.data.name,
        [
          Validators.required,
          Validators.minLength(this.nameMinLength),
          Validators.maxLength(this.nameMaxLength)
        ]
      ],
      id: this.data.id
    })
  }

  OnSubmit() {
    let category: Category = this.categoryForm.value;

    this.categoryService.edit(category)
      .subscribe(_ => {
        this.dialogRef.close(); //It closes successfully
      })
  }

  get name(): AbstractControl {
    return this.categoryForm.get('name');
  }
}
