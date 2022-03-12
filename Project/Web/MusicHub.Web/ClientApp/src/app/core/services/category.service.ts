import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import Category from 'src/app/components/shared/models/category';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  private route: string = 'Category';

  constructor(private http: HttpClient) { }

  all() {
    return this.http.get<Category[]>(this.route);
  }

  create(category: Category) {
    return this.http.post(this.route, category);
  }

  getById(id: number) {
    return this.http.get<Category>(`${this.route}/${id}`);
  }

  edit(category: Category) {
    return this.http.put(`${this.route}/${category.id}`, category);
  }

  delete(id: number) {
    return this.http.delete(`${this.route}/${id}`);
  }
}
