import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup } from '@angular/forms';
import { BarChart } from '../../../charts/models/barChart';
import { ChartDataFilter } from '../../../charts/models/chartDataFilter';
import { LineChart } from '../../../charts/models/lineChart';
import { PieChart } from '../../../charts/models/pieChart ';
import { CategoryAnalysisService } from '../../services/category-analysis.service';

@Component({
  selector: 'app-category-analysis',
  templateUrl: './category-analysis.component.html',
  styleUrls: ['./category-analysis.component.css']
})
export class CategoryAnalysisComponent implements OnInit {
  getTopCategoriesByUploadMusicsCountForMonthsChart: LineChart;
  getMusicsInCategoriesChart: PieChart;
  getCategoriesWithMusicsAllTime: PieChart;
  getTopCategoriesByPlayedMusicsCountForMonths: LineChart;
  getCreatedCategoriesCountsByMonths: PieChart;
  getCategoriesByPlayedMusicsCountAllTime: PieChart;
  analysisForm: FormGroup


  view: any[] = [700, 370];

  constructor(private categoryAnalysisService: CategoryAnalysisService,
    private formBuilder: FormBuilder) {
  
  }

  ngOnInit(): void {
    this.analysisForm = this.formBuilder.group({
      createdOnFrom: [
        new Date()
      ],
      createdOnTo: [
        new Date()
      ],
      countOfMonthsBefore: [
        3
      ],
      countOfSeries: [
        3
      ],
    })

    this.getChartsData();
  }

  OnSubmit() {
    this.getChartsData();
  }

  getChartsData() {
    let filter: ChartDataFilter = this.analysisForm.value;
    console.log(filter);
    this.categoryAnalysisService.getMusicsInCategories(filter).subscribe(r => {
      this.getMusicsInCategoriesChart = r;
    });

    this.categoryAnalysisService.getTopCategoriesByUploadMusicsCountForMonths(filter).subscribe(r => {
      this.getTopCategoriesByUploadMusicsCountForMonthsChart = r;
    });

    this.categoryAnalysisService.getTopCategoriesByPlayedMusicsCountForMonths(filter).subscribe(r => {
      this.getTopCategoriesByPlayedMusicsCountForMonths = r;
    });

    this.categoryAnalysisService.getCategoriesWithMusicsAllTime(filter).subscribe(r => {
      this.getCategoriesWithMusicsAllTime = r;
    });

    this.categoryAnalysisService.getCreatedCategoriesCountsByMonths(filter).subscribe(r => {
      this.getCreatedCategoriesCountsByMonths = r;
    });

    this.categoryAnalysisService.getCategoriesByPlayedMusicsCountAllTime(filter).subscribe(r => {
      this.getCategoriesByPlayedMusicsCountAllTime = r;
    });
    
  }

  get createdOnFrom(): AbstractControl {
    return this.analysisForm.get('createdOnFrom');
  }

  get createdOnTo(): AbstractControl {
    return this.analysisForm.get('createdOnTo');
  }

  get countOfMonthsBefore(): AbstractControl {
    return this.analysisForm.get('countOfMonthsBefore');
  }

  get countOfSeries(): AbstractControl {
    return this.analysisForm.get('countOfSeries');
  }
}
