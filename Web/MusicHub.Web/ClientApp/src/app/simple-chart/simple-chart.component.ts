import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-simple-chart',
  templateUrl: './simple-chart.component.html',
  styleUrls: ['./simple-chart.component.css']
})
export class SimpleChartComponent{
  constructor() {
  }

  productSalesMulti: any =[
    {
      "name": "book",
      "series": [
        {
          "name": "January",
          "value": 125
        }, {
          "name": "February",
          "value": 197
        }, {
          "name": "March",
          "value": 209
        }
      ]
    }, {
  "name": "graphic card",
    "series": [
      {
        "name": "January",
        "value": 210
      }, {
        "name": "February",
        "value": 255
      }, {
        "name": "March",
        "value": 203
      }
    ]
}, {
  "name": "desk",
    "series": [
      {
        "name": "January",
        "value": 89
      }, {
        "name": "February",
        "value": 105
      }, {
        "name": "March",
        "value": 66
      }
    ]
}, {
  "name": "laptop",
    "series": [
      {
        "name": "January",
        "value": 178
      }, {
        "name": "February",
        "value": 165
      }, {
        "name": "March",
        "value": 144
      }
    ]
}, {
  "name": "monitor",
    "series": [
      {
        "name": "January",
        "value": 144
      }, {
        "name": "February",
        "value": 250
      }, {
        "name": "March",
        "value": 133
      }
    ]
}
];
  view: any[] = [700, 370];

  // options
  legend: boolean = true;
  showLabels: boolean = true;
  animations: boolean = true;
  xAxis: boolean = true;
  yAxis: boolean = true;
  showYAxisLabel: boolean = true;
  showXAxisLabel: boolean = true;
  xAxisLabel: string = 'Products';
  yAxisLabel: string = 'Sales';
  timeline: boolean = true;

  colorScheme = {
    domain: ['#704FC4', '#4B852C', '#B67A3D', '#5B6FC8', '#25706F']
  };

  onSelect(event) {
    console.log(event);
  }

  onActivate(data): void {
    console.log('Activate', JSON.parse(JSON.stringify(data)));
  }
}
