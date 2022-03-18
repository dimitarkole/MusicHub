import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup } from '@angular/forms';
import { ChartDataFilter } from '../../../charts/models/chartDataFilter';
import { LineChart } from '../../../charts/models/lineChart';
import { PieChart } from '../../../charts/models/pieChart ';
import { Music } from '../../models/music';
import { MusicPagination } from '../../models/musicPagination';
import { OwnMusicsAnalysisService } from '../../services/own-musics-analysis.service';

@Component({
  selector: 'app-own-musics-analysis',
  templateUrl: './own-musics-analysis.component.html',
  styleUrls: ['./own-musics-analysis.component.css']
})
export class OwnMusicsAnalysisComponent implements OnInit {
  getCreatedMusicByCategoriesCountsAllTime: PieChart;
  getTopCreatedMusicByCategoriesCountForMonths: LineChart;
  topPlayedMusics: MusicPagination;
  analysisForm: FormGroup;

  view: any[] = [700, 370];

  constructor(private ownMusicsAnalysisService: OwnMusicsAnalysisService,
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
    this.ownMusicsAnalysisService.getCreatedMusicByCategoriesCountsAllTime().subscribe(r => {
      this.getCreatedMusicByCategoriesCountsAllTime = r;
    });

    this.ownMusicsAnalysisService.getTopCreatedMusicByCategoriesCountForMonths(filter).subscribe(r => {
      this.getTopCreatedMusicByCategoriesCountForMonths = r;
    });

    this.getTopPlayedMusicsPerPage(1);
  }

  public getTopPlayedMusicsPerPage(page: number): void {
    this.ownMusicsAnalysisService.getTopPlayedMusicsAllTime(page).subscribe(r => {
      this.topPlayedMusics = r;
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
