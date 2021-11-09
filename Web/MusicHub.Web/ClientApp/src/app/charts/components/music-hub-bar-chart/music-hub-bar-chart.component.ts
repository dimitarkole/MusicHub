import { Component, Input, OnInit } from '@angular/core';
import { BarChart } from '../../models/barChart';

@Component({
  selector: 'app-music-hub-bar-chart',
  templateUrl: './music-hub-bar-chart.component.html',
  styleUrls: ['./music-hub-bar-chart.component.css']
})
export class MusicHubBarChartComponent implements OnInit {
  @Input() chart: BarChart;
  showDataLabel: boolean = true;
  view: any[] = [600, 370];

  // options
  legend: boolean = true;

  xAxis: boolean = true;
  yAxis: boolean = true;

  showXAxisLabel: boolean = true;
  showYAxisLabel: boolean = true;

  trimXAxisTicks: boolean = false;
  trimYAxisTicks: boolean = false;
  rotateXAxisTicks: boolean = false;

  animations: boolean = true; // animations on load

  showGridLines: boolean = true; // grid lines

  gradient: boolean = false;
  schemeType: string = 'ordinal'; // 'ordinal' or 'linear'

  roundEdges: boolean = false;
  constructor() {
    console.log(this.chart);
  }

  ngOnInit(): void {
  }
}
