import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { AuthService } from '../../../core/services/auth.service';
import Category from '../../models/category';
import { CategoryService } from '../../services/category.service';

@Component({
  selector: 'app-category-delete',
  templateUrl: './category-delete.component.html',
  styleUrls: ['./category-delete.component.css']
})
export class CategoryDeleteComponent implements OnInit {
  category: Category;
  nameMinLength = 2;
  nameMaxLength = 30;
  categoryForm: FormGroup

  constructor(
    private formBuilder: FormBuilder,
    private categoryService: CategoryService,
    public modal: NgbActiveModal,
    private router: Router,
    private authService: AuthService) {
  }

  ngOnInit() {
    this.categoryForm = this.formBuilder.group({
      name: [
        this.category.name,
        [
          Validators.required,
          Validators.minLength(this.nameMinLength),
          Validators.maxLength(this.nameMaxLength)
        ]
      ],
      id: this.category.id
    })
  }

  OnSubmit() {
    let category: Category = this.categoryForm.value;

    this.categoryService.edit(category)
      .subscribe(_ => {
        this.modal.close(); //It closes successfully
      })
  }

  get name(): AbstractControl {
    return this.categoryForm.get('name');
  }

}
