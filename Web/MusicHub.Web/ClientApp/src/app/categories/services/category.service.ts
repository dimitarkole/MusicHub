import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import Category from '../models/category';
import CategoryPagination from '../models/categoryPagination';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  private route: string = 'Category';

  constructor(private http: HttpClient) { }

  allByPage(page: number) {
    return this.http.get<CategoryPagination>(`${this.route}/GetAll/${page}`);
  }

  all() {
    return this.http.get<Category[]>(`${this.route}`);
  }

  create(category: Category) {
    return this.http.post(this.route, category);
  }

  getById(id: string) {
    return this.http.get<Category>(`${this.route}/${id}`);
  }

  edit(category: Category) {
    return this.http.put(`${this.route}/${category.id}`, category);
  }

  delete(id: string) {
    return this.http.delete(`${this.route}/${id}`);
  }
}
