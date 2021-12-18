import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Observable } from 'rxjs';
import Category from '../../models/category';
import { CategoryService } from '../../services/category.service';

@Component({
  selector: 'app-filter-category',
  templateUrl: './filter-category.component.html',
  styleUrls: ['./filter-category.component.css']
})
export class FilterCategoryComponent implements OnInit {
  @Output() selectedCategory = new EventEmitter();
  categories$: Observable<Category[]>

  constructor(private categoryService: CategoryService) {
    this.categories$ = categoryService.all();
  }

  ngOnInit() {
  }

  selectCategory(categoryId: string) {
    this.selectedCategory.emit(categoryId);
  }
}
