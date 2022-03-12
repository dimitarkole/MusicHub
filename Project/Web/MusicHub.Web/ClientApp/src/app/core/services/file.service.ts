import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import FileInfo from '../../components/shared/models/file';

@Injectable({
  providedIn: 'root'
})
export class FileService {
  private route: string = 'File';

  constructor(private http: HttpClient) { }

  create(file: File, path: string) {
    const formData: FormData = new FormData();
    formData.append("file", file, file.name);
    formData.append("path", path);
    return this.http.post<string>(`${this.route}`, formData);
  }

  createList(files: File[], path: string) {
    const formData: FormData = new FormData();
    for (let i = 0; i < files.length; i++) {
      formData.append(i + "", files[i])
    }
    formData.append("path", path);

    return this.http.post<string[]>(`${this.route}/CreateList`, formData);
  }

  deleteFile(fileInfo: FileInfo) {
    return this.http.post(`${this.route}/Delete`, fileInfo);
  }
}
