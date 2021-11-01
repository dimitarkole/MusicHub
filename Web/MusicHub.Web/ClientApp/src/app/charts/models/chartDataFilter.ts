import { ChartData } from "./chartData";

export interface ChartDataFilter {
  createdOnFrom: Date,
  createdOnTo: Date,
  countOfSeries: number,
  countOfMonthsBefore: number,
}
