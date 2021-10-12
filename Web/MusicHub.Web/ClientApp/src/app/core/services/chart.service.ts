import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Chart } from '../../shared/models/chart';

@Injectable({
  providedIn: 'root'
})
export class ChartService {
  private route: string = 'Chart';

  constructor(private http: HttpClient) { }

  Index<TValue>() {
    return this.http.get<Chart<TValue>>(`${this.route}`);
  }
}
