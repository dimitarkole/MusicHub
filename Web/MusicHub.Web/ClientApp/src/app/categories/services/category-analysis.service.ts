import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BarChart } from '../../charts/models/barChart';
import { ChartDataFilter } from '../../charts/models/chartDataFilter';
import { LineChart } from '../../charts/models/lineChart';
import { PieChart } from '../../charts/models/pieChart ';
import { setQueryString } from '../../common/QueryString';

@Injectable({
  providedIn: 'root'
})
export class CategoryAnalysisService {

  private route: string = 'CategoryAnalysis';

  constructor(private http: HttpClient) { }

  getMusicsInCategories(filter: ChartDataFilter) {
    return this.http.get<PieChart>(`${this.route}/GetCategoriesWithMusicsCount`, { params: setQueryString(filter) });
  }

  getCreatedCategoriesCountsByMonths(filter: ChartDataFilter) {
    return this.http.get<PieChart>(`${this.route}/getCreatedCategoriesCountsByMonths`, { params: setQueryString(filter) });
  }

  getTopCategoriesByUploadMusicsCountForMonths(filter: ChartDataFilter) {
    return this.http.get<LineChart>(`${this.route}/GetTopCategoriesByUploadMusicsCountForMonths`, { params: setQueryString(filter) });
  }

  getCategoriesWithMusicsAllTime(filter: ChartDataFilter) {
    return this.http.get<PieChart>(`${this.route}/GetCategoriesWithMusicsAllTime`);
  }

  getTopCategoriesByPlayedMusicsCountForMonths(filter: ChartDataFilter){
    return this.http.get<LineChart>(`${this.route}/GetTopCategoriesByPlayedMusicsCountForMonths`, { params: setQueryString(filter) });
  }

  getCategoriesByPlayedMusicsCountAllTime(filter: ChartDataFilter) {
    return this.http.get<PieChart>(`${this.route}/getCategoriesByPlayedMusicsCountAllTime`, { params: setQueryString(filter) });
  }
}
