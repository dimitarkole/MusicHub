import { Component, OnInit } from '@angular/core';
import { LineChartData } from '../../models/lineChartData';
import { ChartService } from '../../services/chart.service';

@Component({
  selector: 'app-music-hub-line-chart',
  templateUrl: './music-hub-line-chart.component.html',
  styleUrls: ['./music-hub-line-chart.component.css']
})
export class MusicHubLineChartComponent implements OnInit {
  chartData: LineChartData;

  view: any[] = [700, 370];

  // options
  legend: boolean = true;
  showLabels: boolean = true;
  animations: boolean = true;
  xAxis: boolean = true;
  yAxis: boolean = true;
  showYAxisLabel: boolean = true;
  showXAxisLabel: boolean = true;
  timeline: boolean = true;

  constructor(private chartService: ChartService) {

  }

  ngOnInit(): void {
    this.chartService.Get().subscribe(c => {
      console.log(c);
      this.chartData = c;
    });
  }

  onSelect(event) {
    console.log(event);
  }

  onActivate(data): void {
    console.log('Activate', JSON.parse(JSON.stringify(data)));
  }
}
