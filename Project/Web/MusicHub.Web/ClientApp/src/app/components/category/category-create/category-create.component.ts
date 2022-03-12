import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, AbstractControl } from '@angular/forms';
import { Router } from '@angular/router';
import Category from 'src/app/components/shared/models/category';
import { CategoryService } from 'src/app/core/services/category.service';
import { AuthService } from '../../../core/services/auth.service';

@Component({
  selector: 'app-category-create',
  templateUrl: './category-create.component.html',
  styleUrls: ['./category-create.component.css']
})

export class CategoryCreateComponent implements OnInit {
  nameMinLength = 2;
  nameMaxLength = 30;
  categoryForm: FormGroup

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private categoryService: CategoryService,
    private authService: AuthService) {
    if ((authService.isAuth == false) || (authService.isAdmin == false)) {
      this.router.navigate(['']);
    }
  }

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

  formHandler() {
    let category: Category = this.categoryForm.value;

    this.categoryService.create(category)
      .subscribe(_ => {
        this.router.navigate(['category', 'all']);
      })

    this.categoryForm.reset();
  }

  get name(): AbstractControl {
    return this.categoryForm.get('name');
  }
}
