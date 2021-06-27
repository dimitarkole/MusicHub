import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Permission } from '../../shared/models/permission';
import { PermissionPagination } from '../../shared/models/permissionPagination';

@Injectable({
  providedIn: 'root'
})
export class PermissionService {
  private route: string = 'Permissions';

  constructor(private http: HttpClient) { }

  getById(id: string) {
    return this.http.get<Permission>(`${this.route}/GetById/${id}`);
  }

  all() {
    return this.http.get<Permission[]>(`${this.route}/All`);
  }
}
