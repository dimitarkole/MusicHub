import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup } from '@angular/forms';
import { ChartDataFilter } from '../../../charts/models/chartDataFilter';
import { LineChart } from '../../../charts/models/lineChart';
import { PieChart } from '../../../charts/models/pieChart ';
import { RoleAnalysisService } from '../../services/role-analysis.service';

@Component({
  selector: 'app-role-analysis',
  templateUrl: './role-analysis.component.html',
  styleUrls: ['./role-analysis.component.css']
})
export class RoleAnalysisComponent implements OnInit {
  getRolesByCountOfUsers: PieChart;
  getRolesByCountOfPermissions: PieChart;
  getRolesByUsersForMonths: LineChart;
  getRolesByAverageCountOfUsersForWeek: LineChart;
  getRolesByUsersLastWeek: LineChart;

  analysisForm: FormGroup;
  view: any[] = [700, 370];

  constructor(private roleAnalysisService: RoleAnalysisService,
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
    this.roleAnalysisService.getRolesByCountOfUsers().subscribe(r => {
      this.getRolesByCountOfUsers = r;
    });

    this.roleAnalysisService.getRolesByCountOfPermissions().subscribe(r => {
      this.getRolesByCountOfPermissions = r;
    });

    this.roleAnalysisService.getRolesByUsersForMonths(filter).subscribe(r => {
      this.getRolesByUsersForMonths = r;
    });

    this.roleAnalysisService.getRolesByAverageCountOfUsersForWeek(filter).subscribe(r => {
      this.getRolesByAverageCountOfUsersForWeek = r;
    });

    this.roleAnalysisService.getRolesByUsersLastWeek().subscribe(r => {
      this.getRolesByUsersLastWeek = r;
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
