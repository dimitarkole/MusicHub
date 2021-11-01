import { Component, Input, OnInit } from '@angular/core';
import { LineChart } from '../../models/lineChart';
import { LineChartData } from '../../models/lineChartData';
import { ChartService } from '../../services/chart.service';

@Component({
  selector: 'app-music-hub-line-chart',
  templateUrl: './music-hub-line-chart.component.html',
  styleUrls: ['./music-hub-line-chart.component.css']
})
export class MusicHubLineChartComponent implements OnInit {
  @Input() chart: LineChart;
  view: any[] = [600, 400];

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
   
  }

  onSelect(event) {
    console.log(event);
  }

  onActivate(data): void {
    console.log('Activate', JSON.parse(JSON.stringify(data)));
  }
}
