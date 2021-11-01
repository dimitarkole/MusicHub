import { Component, Input, OnInit } from '@angular/core';
import { PieChart } from '../../models/pieChart ';

@Component({
  selector: 'app-music-hub-pie-chart',
  templateUrl: './music-hub-pie-chart.component.html',
  styleUrls: ['./music-hub-pie-chart.component.css']
})
export class MusicHubPieChartComponent implements OnInit {
  @Input() chart: PieChart;
  view: any[] = [600, 270];
   // options
  showLegend: boolean = true;
  showLabels: boolean = true;

  gradient: boolean = false;
  isDoughnut: boolean = true;

  legendPosition: string = 'below';
  constructor() {
    
  }

  ngOnInit(): void {
  }
}
