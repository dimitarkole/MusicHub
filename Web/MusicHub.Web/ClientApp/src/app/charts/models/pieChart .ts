import { BarChartSeries } from "./barChartSeries";
import { PieChartSeries } from "./pieChartSeries";

export interface PieChart{
  name: string,
  series: PieChartSeries[],
}
