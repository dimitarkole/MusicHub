import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ChartDataFilter } from '../../charts/models/chartDataFilter';
import { LineChart } from '../../charts/models/lineChart';
import { PieChart } from '../../charts/models/pieChart ';
import { setQueryString } from '../../common/QueryString';
import { Music } from '../models/music';
import { MusicPagination } from '../models/musicPagination';

@Injectable({
  providedIn: 'root'
})
export class OwnMusicsAnalysisService {
  private route: string = 'OwnMusicAnalysis';

  constructor(private http: HttpClient) { }

  getCreatedMusicByCategoriesCountsAllTime() {
    return this.http.get<PieChart>(`${this.route}/GetCreatedMusicByCategoriesCountsAllTime`);
  }

  getTopCreatedMusicByCategoriesCountForMonths(filter: ChartDataFilter) {
    return this.http.get<LineChart>(`${this.route}/GetTopCreatedMusicByCategoriesCountForMonths`, { params: setQueryString(filter) });
  }

  getTopPlayedMusicsAllTime(page: number) {
    return this.http.get<MusicPagination>(`${this.route}/GetTopPlayedMusicsAllTime/${page}`);
  }
  
}
