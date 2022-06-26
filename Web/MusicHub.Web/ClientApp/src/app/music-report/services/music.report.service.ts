import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { setQueryString } from '../../common/QueryString';
import ReportMusic from '../models/musicReport';
import { ReportMusicPagination } from '../models/musicReportPagination';

@Injectable({ 
  providedIn: 'root'
})
export class MusicReportService {
  private route: string = 'ReportMusic';

  constructor(private http: HttpClient) { }

  getById(id: string) {
    return this.http.get<ReportMusic>(`${this.route}/GetById/${id}`);
  }

  getAllReportedMusic(page: number) {
    return this.http.get<ReportMusicPagination>(`${this.route}/GetAllReportedMusic/${page}`);
  }

  filterReportedMusic(page: number, reportMusic: ReportMusic) {
    return this.http.get<ReportMusicPagination>(`${this.route}/FilterReportedMusic/${page}`, { params: setQueryString(reportMusic) });
  }

  getAllReported(musicId:string, page: number) {
    return this.http.get<ReportMusicPagination>(`${this.route}/GetAllReported/${musicId}/${page}`);
  }

  filterReported(musicId: string, page: number, reportMusic: ReportMusic) {
    return this.http.get<ReportMusicPagination>(`${this.route}/FilterReported/${musicId}/${page}`, { params: setQueryString(reportMusic) });
  }

  create(reportMusic: ReportMusic) {
    return this.http.post<string>(`${this.route}`, reportMusic);
  }

  delete(id: string) {
    return this.http.delete(`${this.route}/${id}`);
  }

  deleteMusic(musicId: string) {
    return this.http.delete(`${this.route}/DeleteMusic/${musicId}`);
  }
}
