import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { setQueryString } from '../../common/QueryString';
import FileInfo from '../../shared/models/fileInfo';

@Injectable({
  providedIn: 'root'
})
export class MegaStorageFileService {
  private route: string = 'LocalStorage';
  //private route: string = 'MegaStorage';

  constructor(private http: HttpClient) { }

  create(file: File, path: string) {
    const formData: FormData = new FormData();
    formData.append("file", file, file.name);
    formData.append("path", path);
    return this.http.get<string>(`${this.route}`, { params: setQueryString(formData) });
  }

  createList(files: File[], path: string) {
    const formData: FormData = new FormData();
    for (let i = 0; i < files.length; i++) {
      formData.append(i + "", files[i])
    }
    formData.append("path", path);

    return this.http.get<string[]>(`${this.route}/CreateList`, { params: setQueryString(formData) });
  }

  deleteFile(fileInfo: FileInfo) {
    return this.http.post(`${this.route}/Delete`, fileInfo);
  }
/*
  create(file: File, path: string) {
    const formData: FormData = new FormData();
    formData.append("file", file, file.name);
    formData.append("path", path);
    return this.http.post<string>(`${this.route}`, formData);
  }

  download(fileInfo: FileInfo) {
    return this.http.post(`${this.route}/Download`, fileInfo);
  }

  deleteFile(fileInfo: FileInfo) {
    return this.http.post(`${this.route}/Delete`, fileInfo);
  }*/
}
