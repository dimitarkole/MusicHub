import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Role } from '../../shared/models/role';
import { RolePagination } from '../models/rolePagination';

@Injectable({
  providedIn: 'root'
})
export class RoleService {
  private route: string = 'Role';

  constructor(private http: HttpClient) { }

  getById(id: string) {
    return this.http.get<Role>(`${this.route}/${id}`);
  }

  all(page: number) {
    return this.http.get<RolePagination>(`${this.route}/Get/${page}`);
  }

  getAll() {
    return this.http.get<Role[]>(`${this.route}/GetAll`);
  }

  filter(page: number, role: Role) {
    return this.http.post<RolePagination>(`${this.route}/Filter/${page}`, role);
  }

  create(role: Role) {
    return this.http.post(this.route, role);
  }

  edit(roleId: number, role: Role) {
    return this.http.put(`${this.route}/${roleId}`, role);
  }

  delete(id: string) {
    return this.http.delete(`${this.route}/${id}`);
  }
}
