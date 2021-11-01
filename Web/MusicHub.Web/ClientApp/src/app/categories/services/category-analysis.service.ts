import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BarChart } from '../../charts/models/barChart';
import { ChartDataFilter } from '../../charts/models/chartDataFilter';
import { LineChart } from '../../charts/models/lineChart';
import { PieChart } from '../../charts/models/pieChart ';

@Injectable({
  providedIn: 'root'
})
export class CategoryAnalysisService {

  private route: string = 'CategoryAnalysis';

  constructor(private http: HttpClient) { }

  getMusicsInCategories(filter: ChartDataFilter) {
    return this.http.post<PieChart>(`${this.route}/GetCategoriesWithMusicsCount`, filter);
  }

  getCreatedCategoriesCountsByMonths(filter: ChartDataFilter) {
    return this.http.post<PieChart>(`${this.route}/getCreatedCategoriesCountsByMonths`, filter);
  }

  getTopCategoriesByUploadMusicsCountForMonths(filter: ChartDataFilter) {
    return this.http.post<LineChart>(`${this.route}/GetTopCategoriesByUploadMusicsCountForMonths`, filter);
  }

  getCategoriesWithMusicsAllTime(filter: ChartDataFilter) {
    return this.http.get<PieChart>(`${this.route}/GetCategoriesWithMusicsAllTime`);
  }

  getTopCategoriesByPlayedMusicsCountForMonths(filter: ChartDataFilter){
    return this.http.post<LineChart>(`${this.route}/GetTopCategoriesByPlayedMusicsCountForMonths`, filter);
  }

  getCategoriesByPlayedMusicsCountAllTime(filter: ChartDataFilter) {
    return this.http.post<PieChart>(`${this.route}/getCategoriesByPlayedMusicsCountAllTime`, filter);
  }
}
