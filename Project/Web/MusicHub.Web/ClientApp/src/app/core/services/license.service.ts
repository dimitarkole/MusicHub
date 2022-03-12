import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import License from '../../components/shared/models/license';
import LicenseFile from '../../components/shared/models/licenseFile';
import { LicensePagination } from '../../components/shared/models/licensePagination';
import { LicenseStatus } from '../../components/shared/models/LicenseStatus';

@Injectable({
  providedIn: 'root'
})
export class LicenseService {
  private route: string = 'License';

  constructor(private http: HttpClient) { }

  getById(id: string) {
    return this.http.get<License>(`${this.route}/GetById/${id}`);
  }

  all(page: number) {
    return this.http.get<LicensePagination>(`${this.route}/Get/${page}`);
  }

  own(page: number) {
    return this.http.get<LicensePagination>(`${this.route}/GetOwn/${page}`);
  }

  getOwnApproved() {
    return this.http.get<License[]>(`${this.route}/GetOwnApproved`);
  }

  filterAll(page: number, license: License) {
    return this.http.post<LicensePagination>(`${this.route}/Filter/${page}`, license);
  }

  filterOwn(page: number, license: License) {
    return this.http.post<LicensePagination>(`${this.route}/FilterOwn/${page}`, license);
  }

  create(license: License) {
    return this.http.post<string>(`${this.route}/Post`, license);
  }

  edit(license: License, id: string) {
    return this.http.put(`${this.route}/Put/${id}`, license);
  }

  changeStatus(licenseStatus: LicenseStatus, id: string) {
    return this.http.put(`${this.route}/ChangeStatus/${id}`, licenseStatus);
  }

  delete(id: string) {
    return this.http.delete(`${this.route}/${id}`);
  }

  createFile(licenseFile: LicenseFile) {
    console.log("licenseFile");
    console.log(licenseFile);
    return this.http.post(`${this.route}/PostFiles`, licenseFile);
  }

  deleteFile(id: string) {
    return this.http.delete(`${this.route}/DeleteFile/${id}`);
  }
}
