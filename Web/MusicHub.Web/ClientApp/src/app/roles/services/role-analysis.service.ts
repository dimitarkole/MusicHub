import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ChartData } from '../../charts/models/chartData';
import { ChartDataFilter } from '../../charts/models/chartDataFilter';
import { LineChart } from '../../charts/models/lineChart';
import { PieChart } from '../../charts/models/pieChart ';

@Injectable({
  providedIn: 'root'
})
export class RoleAnalysisService {
  private route: string = 'RoleAnalysis';

  constructor(private http: HttpClient) { }

  getRolesByCountOfUsers() {
    return this.http.get<PieChart>(`${this.route}/GetRolesByCountOfUsers`);
  }

  getRolesByCountOfPermissions() {
    return this.http.get<PieChart>(`${this.route}/GetRolesByCountOfPermissions`);
  }

  getRolesByUsersForMonths(filter: ChartDataFilter) {
    return this.http.post<LineChart>(`${this.route}/GetRolesByUsersForMonths`, filter);
  }

  getRolesByAverageCountOfUsersForWeek(filter: ChartDataFilter) {
    return this.http.post<LineChart>(`${this.route}/GetRolesByAverageCountOfUsersForWeek`, filter);
  }

  getRolesByUsersLastWeek() {
    return this.http.get<LineChart>(`${this.route}/GetRolesByUsersLastWeek`);
  }
}
