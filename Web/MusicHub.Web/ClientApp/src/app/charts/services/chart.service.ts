import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { LineChartData } from '../models/lineChartData';

@Injectable({
  providedIn: 'root'
})
export class ChartService {
  private route: string = 'Chart';

  constructor(private http: HttpClient) { }

  Get() {
    return this.http.get<LineChartData>(`${this.route}/Get`);
  }
}
